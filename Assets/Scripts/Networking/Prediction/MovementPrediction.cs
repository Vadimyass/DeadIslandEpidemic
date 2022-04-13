using Gameplay.Character.MovementControllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    public class MovementPrediction : MonoBehaviour
    {
        public struct ClientState
        {
            public Vector3 positionVector;
            public Vector3 rotationVector;
        }

        private struct StateMessage
        {
            public float delivery_time;
            public uint tick_number;
            public Vector3 position;
            public Vector3 rotation;
        }

        public struct InputMessage
        {
            public float deliveryTime;
            public uint startTickNumber;

            public Vector3 inputs;
        }

        public static  GameObject client_player;
        public float latency = 0.1f;
        public float packet_loss_chance = 0.05f;

        // client specific
        public bool client_enable_corrections = true;
        public bool client_correction_smoothing = true;
        public bool client_send_redundant_inputs = true;
        private float client_timer;
        private uint client_tick_number;
        private uint client_last_received_state_tick;
        private const int c_client_buffer_size = 50;
        private ClientState[] client_state_buffer; // client stores predicted moves here
        private InputMessage[] client_input_buffer; // client stores predicted inputs here
        private Queue<StateMessage> client_state_msgs;
        private Vector3 client_pos_error;
        private Quaternion client_rot_error;

        private Queue<InputMessage> server_input_msgs;


        private void Start()
        {
            this.client_timer = 0.0f;
            this.client_tick_number = 0;
            this.client_last_received_state_tick = 0;
            this.client_state_buffer = new ClientState[c_client_buffer_size];
            this.client_input_buffer = new InputMessage[c_client_buffer_size];
            this.client_state_msgs = new Queue<StateMessage>();
            this.client_pos_error = Vector3.zero;
            this.client_rot_error = Quaternion.identity;

            this.server_input_msgs = new Queue<InputMessage>();
        }

        public static void SetClientPlayer(GameObject player) => client_player = player;

        private void Update()
        {
            // client update
            float dt = Time.fixedDeltaTime;
            float client_timer = this.client_timer;
            uint client_tick_number = this.client_tick_number;

            client_timer += Time.deltaTime;
            while (client_timer >= dt)
            {
                client_timer -= dt;

                uint buffer_slot = client_tick_number % c_client_buffer_size;

                Vector3 movementVector = client_player.transform.position;

                // sample and store inputs for this tick
                this.client_input_buffer[buffer_slot].inputs = movementVector;

                // store state for this tick, then use current state + input to step simulation
                this.ClientStoreCurrentStateAndStep(
                    ref this.client_state_buffer[buffer_slot],
                    client_player);

                // send input packet to server
                if (Random.value > this.packet_loss_chance)
                {
                    InputMessage input_msg;
                    input_msg.deliveryTime = Time.time + this.latency;
                    input_msg.startTickNumber = this.client_send_redundant_inputs ? this.client_last_received_state_tick : client_tick_number;
                    input_msg.inputs = Vector3.zero;

                    for (uint tick = input_msg.startTickNumber; tick <= client_tick_number; ++tick)
                    {
                        input_msg.inputs = client_input_buffer[tick % c_client_buffer_size].inputs;
                    }
                    this.server_input_msgs.Enqueue(input_msg);
                }

                ++client_tick_number;
            }

            if (this.ClientHasStateMessage())
            {
                StateMessage state_msg = this.client_state_msgs.Dequeue();
                while (this.ClientHasStateMessage()) // make sure if there are any newer state messages available, we use those instead
                {
                    state_msg = this.client_state_msgs.Dequeue();
                }

                this.client_last_received_state_tick = state_msg.tick_number;


                if (this.client_enable_corrections)
                {
                    uint buffer_slot = state_msg.tick_number % c_client_buffer_size;
                    Vector3 position_error = state_msg.position - this.client_state_buffer[buffer_slot].positionVector;
                    float rotation_error = 1.0f - Vector3.Dot(state_msg.rotation, this.client_state_buffer[buffer_slot].rotationVector);

                    if (position_error.sqrMagnitude > 0.0000001f ||
                        rotation_error > 0.00001f)
                    {
                        Debug.Log("Correcting for error at tick " + state_msg.tick_number + " (rewinding " + (client_tick_number - state_msg.tick_number) + " ticks)");
                        // capture the current predicted pos for smoothing
                        Vector3 prev_pos = client_player.transform.position + this.client_pos_error;
                        Quaternion prev_rot = client_player.transform.rotation * this.client_rot_error;

                        // rewind & replay
                        client_player.transform.position = state_msg.position;
                        client_player.transform.forward = state_msg.rotation;

                        uint rewind_tick_number = state_msg.tick_number;
                        while (rewind_tick_number < client_tick_number)
                        {
                            buffer_slot = rewind_tick_number % c_client_buffer_size;
                            this.ClientStoreCurrentStateAndStep(
                                ref this.client_state_buffer[buffer_slot],
                                client_player);

                            ++rewind_tick_number;
                        }

                        // if more than 2ms apart, just snap
                        if ((prev_pos - client_player.transform.position).sqrMagnitude >= 4.0f)
                        {
                            this.client_pos_error = Vector3.zero;
                            this.client_rot_error = Quaternion.identity;
                        }
                        else
                        {
                            this.client_pos_error = prev_pos - client_player.transform.position;
                            this.client_rot_error = Quaternion.Inverse(client_player.transform.rotation) * prev_rot;
                        }
                    }
                }
            }

            this.client_timer = client_timer;
            this.client_tick_number = client_tick_number;

            if (this.client_correction_smoothing)
            {
                this.client_pos_error *= 0.9f;
                this.client_rot_error = Quaternion.Slerp(this.client_rot_error, Quaternion.identity, 0.1f);
            }
            else
            {
                this.client_pos_error = Vector3.zero;
                this.client_rot_error = Quaternion.identity;
            }
        }
        private bool ClientHasStateMessage()
        {
            return this.client_state_msgs.Count > 0 && Time.time >= this.client_state_msgs.Peek().delivery_time;
        }

        private void ClientStoreCurrentStateAndStep(ref ClientState current_state,GameObject movementController)
        {
            current_state.positionVector = movementController.transform.position;
            current_state.rotationVector = movementController.transform.forward;
        }
    }
}

