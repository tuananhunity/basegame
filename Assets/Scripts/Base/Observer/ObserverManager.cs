using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Anh.Obsever
{
    public static class ObserverManager
    {
        private static Dictionary<Type, List<IEventListenerBase>> _subscribers;
        static ObserverManager()
        {
            _subscribers = new();
        }
        public static void AddListener<ObserverEvent>(IEventListener<ObserverEvent> listener) where ObserverEvent : struct
        {
            Type eventType_Key = typeof(ObserverEvent);
            if (!_subscribers.ContainsKey(eventType_Key))
                _subscribers[eventType_Key] = new List<IEventListenerBase>();
            if (!_subscribers[eventType_Key].Contains(listener))
            {
                _subscribers[eventType_Key].Add(listener);
            }
        }
        public static void RemoveListener<ObserverEvent>(IEventListener<ObserverEvent> listener) where ObserverEvent : struct
        {
            Type eventType_Key = typeof(ObserverEvent);
            if (!_subscribers.ContainsKey(eventType_Key))
            {
                return;
            }
            if (_subscribers[eventType_Key].Contains(listener))
            {
                int indexListener = _subscribers[eventType_Key].IndexOf(listener);
                _subscribers[eventType_Key].RemoveAt(indexListener);
                if (_subscribers[eventType_Key].Count == 0)
                    _subscribers.Remove(eventType_Key);
            }
        }
        public static void TriggerEvent<ObserverEvent>(ObserverEvent observerEvent) where ObserverEvent : struct
        {
            Type eventType_Key = typeof(ObserverEvent);
            List<IEventListenerBase> listenerTriggers;
            if (!_subscribers.TryGetValue((eventType_Key), out listenerTriggers))
                return;
            for(int i = 0; i < listenerTriggers.Count; i++)
            {
                (listenerTriggers[i] as IEventListener<ObserverEvent>).OnReceiveEvent(observerEvent);
            }
        }
    }

}