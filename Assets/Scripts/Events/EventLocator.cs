using System;
using System.Collections.Generic;
using UnityEngine;

namespace DeadIsland.Events
{
    public static class EventLocator
    {
        static readonly Dictionary<int, List<IEventObserver>> _observers = new Dictionary<int, List<IEventObserver>>();

        public static void AddObserver(IEventObserver observer)
        {
            var eventType = observer.ObserverType;

            if (!_observers.ContainsKey(eventType))
            {
                _observers.Add(eventType, new List<IEventObserver>());
            }

            //LogCategory.Info($"Bind observer {observer.GetType()}", LogCategory.Category.EventBus);

            _observers[eventType].Add(observer);
        }

        public static void RemoveObserver(IEventObserver observer)
        {
            //LogCategory.Info($"Clear observer {observer.GetType()}", LogCategory.Category.EventBus);

            var eventType = observer.ObserverType;

            if (_observers.ContainsKey(eventType))
            {
                _observers[eventType].Remove(observer);
            }
        }

        public static void Invoke(IEvent @event)
        {
            //LogCategory.Info($"Invoke event {@event.GetType()}", LogCategory.Category.EventBus);
            var eventId = @event.GetType().GetHashCode();
            
            if (_observers.ContainsKey(eventId))
            {
                var observers = _observers[eventId];

                for (int i = 0; i < observers.Count; i++)
                {
                    try
                    {
                        if (!observers[i].Invoke(@event))
                        {
                            observers.RemoveAt(i);
                            i--;
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
            }
        }
    }
}