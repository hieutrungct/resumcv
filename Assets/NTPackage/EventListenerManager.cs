using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NTPackage.EventDispatcher
{
    public enum EventCode{
        ACC_LobbyUDP,
        ACC_LobbyOut,
        ACC_Update_PlayerData,
        ACC_Update_MyPlayerData,
        ACC_ReceiveEmotion,
    }

    public class EventListenerManager : MonoBehaviour
    {
        public NTDictionary<EventCode, NTDictionary<string, Action<object>>> ActionsDictionary = new NTDictionary<EventCode, NTDictionary<string, Action<object>>>();

        public static EventListenerManager instance;
        private void Awake()
        {
            if (EventListenerManager.instance != null)
            {
                Debug.LogWarning("Only 1 instance allow");
                return;
            }
            EventListenerManager.instance = this;
        }

        public void PostEvent(EventCode eventCode,object data = null){
            NTDictionary<string,Action<object>> actions = this.ActionsDictionary.Get(eventCode);
            if(actions == null || actions.Dictionary.Count == 0) return;
            foreach (KeyValuePair<string, System.Action<object>> item in actions.Dictionary)
            {
                try
                {
                    item.Value.Invoke(data);
                }
                catch (System.Exception)
                {
                    actions.Remove(item.Key);
                }
            }
        }

        public void Register(EventCode eventCode, string key,Action<object> callback){
            
            NTDictionary<string, Action<object>> actions = this.ActionsDictionary.Get(eventCode);
            if(actions == null){
                actions = new NTDictionary<string, Action<object>>();
                this.ActionsDictionary.Add(eventCode, actions);
            }
            actions.Add(key, callback);
        }
    }
}

