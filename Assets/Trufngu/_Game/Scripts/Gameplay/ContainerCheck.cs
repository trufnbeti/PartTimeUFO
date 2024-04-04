using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCheck : MonoBehaviour
{
    [SerializeField] private Container owner;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(Constant.TAG_PROP)) {
            Prop prop = CacheComponent.GetProp(other);
            prop.OnEnterContainer(owner);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag(Constant.TAG_PROP)) {
            Prop prop = CacheComponent.GetProp(other);
            prop.OnExitContainer(owner);
        }
    }
}
