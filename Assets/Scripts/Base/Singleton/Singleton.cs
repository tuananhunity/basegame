using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Anh.Base
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject singleton = new GameObject(typeof(T).ToString());
                        instance = singleton.AddComponent<T>();
                    }
                }
                return instance;
            }
        }
        private void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        protected virtual void OnApplicationQuit()
        {
            if (ReferenceEquals(instance, this))
            {
                Destroy(gameObject);
                instance = null;
            }
        }
    }
}
