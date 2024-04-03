using UnityEngine;
using Utilities;

public class Prop : GameUnit
{
    [SerializeField] private DragableRigidbody2DJoint dragable;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask containerLayer;
    

    private bool isCollected;
    public bool IsGrounded { get; private set; }
    public bool IsOnContainer { get; private set; }
    
    public void OnInit() {
        IsGrounded = false;
        isCollected = false;
        IsOnContainer = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag(Constant.TAG_GROUND)) {
            IsGrounded = true;
        }

        if (other.gameObject.CompareTag(Constant.TAG_CONTAINER)) {
            IsOnContainer = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag(Constant.TAG_GROUND)) {
            IsGrounded = false;
        }
    }

    private bool CheckGrounded() {
        return Physics2D.BoxCast(transform.position, new Vector2(1, 0.1f), 0, Vector2.down, radius, groundLayer);
    }

    private void Update() {
        if (!isCollected) {
            if (!IsGrounded && Vector2.Distance(dragable.Rb.velocity, Vector2.zero) < 0.0001f && !dragable.IsDragging && IsOnContainer) {
                isCollected = true;
                LevelManager.Ins.OnPropInBucket(this);
            }
        }
    }
}
