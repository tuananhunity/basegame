using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Anh.Obsever
{
    public struct VTVChanel
    {
        public string nameEvent;
        public object data;
        public VTVChanel(string nameEvent, object data)
        {
            this.nameEvent = nameEvent;
            this.data = data;
        }
        public const string TRIGGERCHANEL = "VTVCHANEL";
    }
    public struct MTPChanel
    {
        public string nameEvent;
        public object data;
        public MTPChanel(string nameEvent, object data)
        {
            this.nameEvent = nameEvent;
            this.data = data;
        }
        public const string TRIGGERCHANEL = "MTPCHANEL";
    }
}