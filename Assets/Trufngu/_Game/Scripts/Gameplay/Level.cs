using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Level : MonoBehaviour {
    [SerializeField] public List<Prop> props = new List<Prop>();
    [SerializeField] public Container container;

    public int TotalProps => props.Count;
    
    public void OnInit() {
        for (int i = 0; i < props.Count; ++i) {
            props[i].OnInit();
        }
    }
}
