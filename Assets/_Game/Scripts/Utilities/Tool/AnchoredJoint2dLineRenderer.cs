using System.Collections;
using UnityEngine;

namespace Utilities
{
    public class AnchoredJoint2dLineRenderer : MonoBehaviour
    {
        public AnchoredJoint2D joint;
        public LineRenderer lineRenderer;
        public Vector3 offsetMainBody;
        public Vector3 offsetConnectedBody;

        private void OnEnable()
        {
            lineRenderer.enabled = true;
        }

        private void OnDisable()
        {
            if (lineRenderer) lineRenderer.enabled = false;
        }

        private void Update()
        {
            lineRenderer.SetPosition(0, joint.connectedBody.transform.TransformPoint(offsetConnectedBody));
            lineRenderer.SetPosition(1, joint.transform.TransformPoint(offsetMainBody));
        }

#if UNITY_EDITOR
        [SerializeField] private bool isUpdateRopeEditor = false;
        [ContextMenu("UpdateRopeEditor")]
        private void OnValidate()
        {
            if (isUpdateRopeEditor)
            {
                UpdateRopeEditor();
            }
        }
        private void UpdateRopeEditor()
        {
            lineRenderer.SetPosition(0, joint.connectedBody.transform.position + offsetConnectedBody);
            lineRenderer.SetPosition(1, joint.transform.position + offsetMainBody);
            UnityEditor.EditorUtility.SetDirty(lineRenderer);
        }
#endif
    }
}