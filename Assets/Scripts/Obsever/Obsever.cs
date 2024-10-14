using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsever
{

    static Dictionary<EventID, List<Action<KeyValuePair<string, object>>>> Events = new();

    public static void AddListener(EventID eventID, Action<KeyValuePair<string, object>> callback){
        if(!Events.ContainsKey(eventID)) Events.Add(eventID, new List<Action<KeyValuePair<string, object>>>());
        Events[eventID].Add(callback);
    }

    public static void RemoveListener(EventID eventID,  Action<KeyValuePair<string, object>> callback){
        if(!Events.ContainsKey(eventID)) return;
        Events[eventID].Remove(callback);
    }

    public static void PostEvent(EventID eventID, KeyValuePair<string, object> pram){
        if(!Events.ContainsKey(eventID)) return;
        foreach( Action<KeyValuePair<string, object>> callback in Events[eventID]){
            callback(pram);
        }
    }


    public static void RemoveAllListener(EventID eventID){
        if(!Events.ContainsKey(eventID)) return;
        Events[eventID].Clear();
        Events.Remove(eventID);
    }
}