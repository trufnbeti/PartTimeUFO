using System;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

public class Prop : GameUnit
{
    [SerializeField] private DragableRigidbody2DJoint dragable;
    private Container container;
    
	public bool IsOnContainer { get; private set; }

    public bool CanCollect => IsOnContainer && Vector2.Distance(dragable.Rb.velocity, Vector2.zero) < 0.75f && !dragable.IsDragging;
    
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
