using System;

namespace DeadIsland.Events
{
    public interface IEventObserver
    {
        int ObserverType { get; }
        void Subscribe(Action<IEvent> handler);
        void Unsubscribe();
        bool Invoke(IEvent @event);
    }
}