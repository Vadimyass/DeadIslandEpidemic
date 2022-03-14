using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace DeadIsland.Events
{
    /// <summary>
    /// Helper class for dispose observers for specific objects
    /// </summary>
    public static class EventObserversDisposer
    {
        private static readonly Dictionary<int, List<IEventObserver>> _bindedObservers = new Dictionary<int, List<IEventObserver>>();

        public static void BindGameEventObserver<T>(this object target, Action<T> action) where T : EventBase
        {
            target.BindEventObserver(new GameEventObserver<T>(action));
        }
        
        public static void BindEventObserver(this object target, IEventObserver observer)
        {
            var targetHash = target.GetHashCode();

            if (!_bindedObservers.ContainsKey(targetHash))
            {
                _bindedObservers[targetHash] = new List<IEventObserver>();           
            }
            
            _bindedObservers[targetHash].Add(observer);
        }

        public static void ClearEventObservers(this object target)
        {
            var targetHash = target.GetHashCode();

            if (_bindedObservers.ContainsKey(targetHash))
            {
                foreach (var observer in _bindedObservers[targetHash])
                {
                    observer.Unsubscribe();
                }

                _bindedObservers.Remove(targetHash);
            }
        }
    }
}