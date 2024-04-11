using UnityEngine;

/// <summary>
/// Drag a Rigidbody2D by selecting one of its colliders by pressing the mouse down.
/// When the collider is selected, add a TargetJoint2D.
/// While the mouse is moving, continually set the target to the mouse position.
/// When the mouse is released, the TargetJoint2D is deleted.`
/// </summary>
public class DragTarget : MonoBehaviour {
	public LayerMask m_DragLayers;

	[Range (0.0f, 100.0f)]
	public float m_Damping = 1.0f;

	[Range (0.0f, 100.0f)]
	public float m_Frequency = 5.0f;

	public bool m_DrawDragLine = true;
	public Color m_Color = Color.cyan;
	
	private TargetJoint2D m_TargetJoint;

	[Header("Use Offset")]
	[SerializeField] private bool isUseOffset;
	[SerializeField] private Vector3 offset;
	private Vector3 currentOffset;
	
	private Prop propDragging;

	public bool IsDragging => propDragging != null;
	
	void Update ()
	{
		// Calculate the world position for the mouse.
		var worldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		
		if (Input.GetMouseButtonDown (0))
		{
			// Fetch the first collider.
			// NOTE: We could do this for multiple colliders.
			var collider = Physics2D.OverlapPoint (worldPos, m_DragLayers);
			if (!collider)
				return;
			propDragging = CacheComponent.GetProp(collider);
			propDragging.DestroyJoint();
			propDragging.IsDragging = true;
			Rigidbody2D rb = propDragging.rb;

			if (!rb) {
				return;
			}

			// Fetch the collider body.
			// var body = collider.attachedRigidbody;
			// if (!body)
			// 	return;

			currentOffset = Vector3.zero;
			
			// Add a target joint to the Rigidbody2D GameObject.
			m_TargetJoint = propDragging.gameObject.AddComponent<TargetJoint2D>();
			m_TargetJoint.dampingRatio = m_Damping;
			m_TargetJoint.frequency = m_Frequency;
			m_TargetJoint.maxForce = 1000;
			m_TargetJoint.autoConfigureTarget = false;

			// Attach the anchor to the local-point where we clicked.
			propDragging.AnchorJoint = m_TargetJoint.transform.InverseTransformPoint (worldPos);
			m_TargetJoint.anchor = propDragging.AnchorJoint;

		}
		else if (Input.GetMouseButtonUp (0)) {
			if (propDragging) {
				LevelManager.Ins.CheckPin(propDragging);
				propDragging.IsDragging = false;
				propDragging = null;
			}
			Destroy (m_TargetJoint);
			m_TargetJoint = null;
			return;
		}
		
		// Update the joint target.
		if (m_TargetJoint) {
			if (isUseOffset) {
				currentOffset = Vector3.Lerp(currentOffset, offset, Time.deltaTime);
				m_TargetJoint.target = worldPos + currentOffset;
			} else {
				m_TargetJoint.target = worldPos;
			}
			

			// Draw the line between the target and the joint anchor.
			if (m_DrawDragLine) {
				Debug.DrawLine(m_TargetJoint.transform.TransformPoint(m_TargetJoint.anchor), worldPos, m_Color);
			}
		}
		
	}
}