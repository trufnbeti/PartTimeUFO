using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace PrototypeGameplayDragPin
{
    public class PinPoint : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Color colorNormal = Color.gray;
        public Color colorHighlight = Color.white;
        public float radius;
        public AnchoredJoint2dLineRenderer prefabAnchoredJoint2DLineRenderer;
        private List<SpringJoint2D> _joints = new List<SpringJoint2D>();
        private List<AnchoredJoint2dLineRenderer> _line = new List<AnchoredJoint2dLineRenderer>();

        private void OnMouseUp()
        {
            Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (IsPointInsidePin(worldMousePos))
            {
                foreach (var joint in _joints)
                {
                    Destroy(joint.gameObject);
                }

                foreach (var line in _line)
                {
                    Destroy(line.gameObject);
                }

                _joints.Clear();
                _line.Clear();
            }
        }

        public void SetHighlight(bool isHighlight)
        {
            spriteRenderer.color = isHighlight ? colorHighlight : colorNormal;
        }

        public bool IsPointInsidePin(Vector2 point) => Vector2.Distance(point, transform.position) < radius;

        public void CreateSpringJoint(DragableRigidbody2DSpringJoint dragObject)
        {
            GameObject obj = new GameObject("Rigidbody2D dragger");
            obj.transform.parent = transform;
            obj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            Rigidbody2D jointBody = obj.AddComponent<Rigidbody2D>();
            jointBody.isKinematic = true;
            SpringJoint2D joint = obj.AddComponent<SpringJoint2D>();

            SpringJoint2D src = dragObject.Joint as SpringJoint2D;

            joint.anchor = src.anchor;
            joint.connectedAnchor = src.connectedAnchor;
            joint.dampingRatio = src.dampingRatio;
            joint.frequency = src.frequency;
            // Don't want our invisible "Rigidbody2D dragger" to collide!
            joint.enableCollision = false;
            joint.connectedBody = dragObject.Rb;
            joint.autoConfigureDistance = src.autoConfigureConnectedAnchor;
            joint.distance = src.distance;

            joint.enabled = true;

            AnchoredJoint2dLineRenderer line = Instantiate(prefabAnchoredJoint2DLineRenderer, transform);

            line.joint = joint;
            line.offsetMainBody = joint.anchor;
            line.offsetConnectedBody = joint.connectedAnchor;
            line.enabled = true;

            _joints.Add(joint);
            _line.Add(line);
        }
        
        public void CreateSpringJoint(Prop dragObject)
        {
            GameObject obj = new GameObject("Rigidbody2D dragger");
            obj.transform.parent = transform;
            obj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            Rigidbody2D jointBody = obj.AddComponent<Rigidbody2D>();
            jointBody.isKinematic = true;
            SpringJoint2D joint = obj.AddComponent<SpringJoint2D>();
            
            joint.anchor = Vector2.zero;
            joint.connectedAnchor = dragObject.AnchorJoint;
            joint.dampingRatio = 1;
            joint.frequency = 5;
            // Don't want our invisible "Rigidbody2D dragger" to collide!
            joint.enableCollision = false;
            joint.connectedBody = dragObject.rb;
            joint.autoConfigureDistance = true;
            joint.distance = 0.5f;

            dragObject.jointPin = joint;

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}