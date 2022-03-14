using System;
using UnityEngine;
using Utils;

namespace DeadIsland.Events
{
    class GameEventObserver<T> : IEventObserver where T : EventBase
    {
        public int ObserverType { get; private set; }

        private WeakReference _reference;
        private Action<T> _handler;

        public GameEventObserver()
        {
        }
        
        public GameEventObserver(Action<T> handler)
        {
            Subscribe(handler);
        }

        public void Subscribe(Action<T> handler)
        {
            _reference = new WeakReference(handler.Target);
            _handler = handler;

            ObserverType = typeof(T).GetHashCode();
            EventLocator.AddObserver(this);
        }

        void IEventObserver.Subscribe(Action<IEvent> handler)
        {
            Subscribe(handler);
        }

        public void Unsubscribe()
        {
            _reference = null;
            _handler = null;
            EventLocator.RemoveObserver(this);
        }

        public bool Invoke(IEvent @event)
        {
            var monoBehaviourTarget = _reference.Target as MonoBehaviour;
            if (!_reference.IsAlive || (monoBehaviourTarget != null && monoBehaviourTarget.IsNull()))
            {
                return false;
            }
            _handler.Invoke(@event as T);
            
            return true;
        }

        public override string ToString()
        {
            return $"Target: {_reference.Target}, Handler Type: {_handler.GetType()}";
        }
    }
}