using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Anh.Obsever
{
    public interface IEventListenerBase { }
    public interface IEventListener<T> : IEventListenerBase
    {
        public void OnReceiveEvent(T eventMessage);
    }
}