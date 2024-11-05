using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Anh.Obsever
{
    [Serializable]
    public class AnhEventChanel
    {
        public object chanel;
        private List<IAnhEventListener> listeners;
        public AnhEventChanel()
        {
            listeners = new List<IAnhEventListener>();
        }
        public void AddListener(IAnhEventListener anhListener)
        {
            if (listeners.Contains(anhListener)) return;
            listeners.Add(anhListener);
        }
        public void RemoveListener(IAnhEventListener anhListener)
        {
            if (!listeners.Contains(anhListener)) return;
            listeners.Remove(anhListener);
        }
        public void PushEvent()
        {
            var mListeners = new List<IAnhEventListener>();
            mListeners.AddRange(listeners);
            foreach (var listener in mListeners)
            {
                try
                {
                    listener.OnReceiveEvent(chanel);
                }
                catch { }
            }
        }
    }
}
