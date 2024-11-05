using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Anh.Obsever
{
    public static class AnhObserver
    {
        private static List<AnhEventChanel> anhEventChanels;
        static AnhObserver()
        {
            anhEventChanels = new();
        }
        public static void AddListener(IAnhEventListener listener, params Type[] chanelTypes)
        {
            foreach (var chanelType in chanelTypes)
            {
                GetChanelToAdd(chanelType).AddListener(listener);
            }
        }
        public static void RemoveListener(IAnhEventListener listener, params Type[] chanelTypes)
        {
            foreach (var chanelType in chanelTypes)
            {
                GetChanelToAdd(chanelType).RemoveListener(listener);
            }
        }
        public static void TriggerEvent(object chanelData)
        {
            GetChanelToTrigger(chanelData).PushEvent();
        }
        private static AnhEventChanel GetChanelToTrigger(object chanelData)
        {
            foreach (var chanelEvent in anhEventChanels)
            {
                if (chanelEvent.chanel as Type == chanelData.GetType())
                {
                    chanelEvent.chanel = chanelData;
                    return chanelEvent;
                }
                else if (chanelEvent.chanel.GetType() == chanelData.GetType())
                {
                    chanelEvent.chanel = chanelData;
                    return chanelEvent;
                }
            }
            return null;
        }
        private static AnhEventChanel GetChanelToAdd(Type chaneltype)
        {
            foreach (var chanelEvent in anhEventChanels)
            {
                if (chanelEvent.chanel as Type == chaneltype)
                {
                    return chanelEvent;
                }
                else if(chanelEvent.chanel.GetType() == chaneltype)
                {
                    return chanelEvent;
                }
            }
        var anhEventChanel = new AnhEventChanel()
        {
            chanel = chaneltype
        };
        anhEventChanels.Add(anhEventChanel);
        return anhEventChanel;
        }
    }
}