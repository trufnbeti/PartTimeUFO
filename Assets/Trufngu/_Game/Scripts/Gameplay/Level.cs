using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Level : MonoBehaviour {
    public List<Prop> props = new List<Prop>();

    public int totalProps => props.Count;
    
    public void OnInit() {
        for (int i = 0; i < props.Count; ++i) {
            props[i].OnInit();
        }
    }
}
