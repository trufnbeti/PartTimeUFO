using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    private List<Prop> props = new List<Prop>();

    public int PropsCount => props.Count;

    public void OnInit() {
        props.Clear();
    }

    public void AddProp(Prop prop) {
        if (!props.Contains(prop)) {
            props.Add(prop);
            LevelManager.Ins.OnPropCollect(prop);
        }
    }

    public void RemoveProp(Prop prop) {
        if (props.Contains(prop)) {
            props.Remove(prop);
            LevelManager.Ins.OnPropExit(prop);
        }
    }

}
