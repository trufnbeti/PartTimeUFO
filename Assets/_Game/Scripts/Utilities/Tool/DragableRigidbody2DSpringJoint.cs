using System.Collections;
using UnityEngine;

namespace Utilities
{
    public class DragableRigidbody2DSpringJoint : DragableRigidbody2DJoint
    {
        [Header("Spring Joint")]
        public float dampingRatio = 5.0f;
        public float frequency = 2.5f;
        public float distance = 3f;

        public AnchoredJoint2dLineRenderer anchoredJoint2DLineRenderer;

        public SpringJoint2D SpringJoint { get; private set; }
        private Rigidbody2D jointBody;

        protected override Joint2D SetUpJoint()
        {
            if (!SpringJoint)
            {
                GameObject obj = new GameObject("Rigidbody2D dragger");
                jointBody = obj.AddComponent<Rigidbody2D>();
                jointBody.isKinematic = true;
                SpringJoint = obj.AddComponent<SpringJoint2D>();
            }

            Vector2 mouseWorldPos = GetMouseWorldPos();

            SpringJoint.transform.position = mouseWorldPos;
            SpringJoint.anchor = Vector2.zero;
            SpringJoint.connectedAnchor = transform.InverseTransformPoint(mouseWorldPos);
            SpringJoint.dampingRatio = dampingRatio;
            SpringJoint.frequency = frequency;
            // Don't want our invisible "Rigidbody2D dragger" to collide!
            SpringJoint.enableCollision = false;
            SpringJoint.connectedBody = Rb;

            if (distance > 0f)
            {
                SpringJoint.autoConfigureDistance = false;
                SpringJoint.distance = distance;
            }
            else
            {
                SpringJoint.autoConfigureDistance = true;
            }

            SpringJoint.enabled = true;

            if (anchoredJoint2DLineRenderer)
            {
                anchoredJoint2DLineRenderer.joint = SpringJoint;
                anchoredJoint2DLineRenderer.offsetMainBody = SpringJoint.anchor;
                anchoredJoint2DLineRenderer.offsetConnectedBody = SpringJoint.connectedAnchor;
                // anchoredJoint2DLineRenderer.enabled = true;
            }

            return SpringJoint;
        }

        protected override void OnUpdateDraggingDynamic()
        {
        }

        protected override void UpdateJointOnEndDrag()
        {
            SpringJoint.connectedBody = null;
            if (anchoredJoint2DLineRenderer)
            {
                anchoredJoint2DLineRenderer.enabled = false;
            }
        }

        protected override void OnUpdateDraggingFixed()
        {
            jointBody.MovePosition(GetMouseWorldPos());
        }
    }
}