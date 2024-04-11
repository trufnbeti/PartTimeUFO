using System;
using System.Collections;
using System.Collections.Generic;
using PrototypeGameplayDragPin;
using UnityEngine;
using Utilities;

public class Level : MonoBehaviour {
    [SerializeField] private List<Prop> props = new List<Prop>();
    [SerializeField] private Container container;
    
    [SerializeField] public List<PinPoint> pinPoints;
    
    public int TotalProps => props.Count;

    public void OnInit() {
        for (int i = 0; i < props.Count; ++i) {
            props[i].OnInit();
        }

        if (container) {
            container.OnInit();
        }
    }
}
