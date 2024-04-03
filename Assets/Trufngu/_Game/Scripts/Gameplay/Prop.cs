using UnityEngine;
using Utilities;

public class Prop : GameUnit
{
    [SerializeField] private DragableRigidbody2DJoint dragable;
    [SerializeField] private float radius;
    [SerializeField] private float size;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask containerLayer;

    private float DistanceCast => radius * size * 0.5f;
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

    private void CheckPropLanded() {
        IsOnContainer =  Physics2D.Raycast(transform.position,  Vector2.down, DistanceCast, containerLayer);
        IsGrounded =  Physics2D.Raycast(transform.position,  Vector2.down, DistanceCast, groundLayer);
    }
    
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(TF.position, TF.position - Vector3.up * DistanceCast);
    }

    private void Update() {
        if (IsGrounded) {
            isCollected = false;
        }
        if (!isCollected) {
            if (!IsGrounded && Vector2.Distance(dragable.Rb.velocity, Vector2.zero) < 0.001f && !dragable.IsDragging) {
                isCollected = true;
                LevelManager.Ins.OnPropInBucket(this);
                Debug.Log("HI");
            }
        }
    }
}
