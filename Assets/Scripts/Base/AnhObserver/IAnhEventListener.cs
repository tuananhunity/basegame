using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Anh.Obsever
{
    public interface IAnhEventListener
    {
        public void OnReceiveEvent(object typeEvent);
    }
}