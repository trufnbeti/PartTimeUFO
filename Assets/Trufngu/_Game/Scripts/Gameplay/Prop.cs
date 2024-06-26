using System;
using PrototypeGameplayDragPin;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

public class Prop : GameUnit
{
    public Rigidbody2D rb;
    
    private Container container;
    
    public Vector2 AnchorJoint { get; set; }
    public SpringJoint2D jointPin;
    public bool IsDragging { get;  set; }
	public bool IsOnContainer { get; private set; }

    public bool CanCollect => IsOnContainer && Vector2.Distance(rb.velocity, Vector2.zero) < 0.75f && !IsDragging;
    
    public void OnInit() {
        IsOnContainer = false;
    }

    public void OnEnterContainer(Container container) {
        this.container = container;
        IsOnContainer = true;
    }
    
    public void OnExitContainer(Container container) {
        if (this.container == container) {
            this.container = null;
            IsOnContainer = false;
        }
    }

    public void DestroyJoint() {
        if (jointPin != null) {
            Destroy(jointPin.gameObject);
            jointPin = null;
        }
    }

    private void Update() {
        if (CanCollect) {
            container.AddProp(this);
        } else {
            if (container) {
                container.RemoveProp(this);
            }
        }
    }

}
