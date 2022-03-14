using UnityEngine;

namespace DeadIsland.Events
{
    public interface IEvent
    {
        void Invoke();
    }

    public abstract class EventBase : IEvent
    {
        public virtual void Invoke()
        {
            EventLocator.Invoke(this);
        }
    }
}