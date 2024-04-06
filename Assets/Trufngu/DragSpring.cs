using UnityEngine;

/// <summary>
/// Drag a Rigidbody2D by selecting one of its colliders by pressing the mouse down.
/// When the collider is selected, add a TargetJoint2D.
/// While the mouse is moving, continually set the target to the mouse position.
/// When the mouse is released, the TargetJoint2D is deleted.`
/// </summary>
public class DragSpring : MonoBehaviour {
	[SerializeField] private SpringJoint2D springJoint;
	public LayerMask m_DragLayers;

	[Range (0.0f, 100.0f)]
	public float m_Damping = 1.0f;

	[Range (0.0f, 100.0f)]
	public float m_Frequency = 5.0f;

	public float distance = 2f;

	public bool m_DrawDragLine = true;
	public Color m_Color = Color.cyan;


	private Prop propDragging;

	private Rigidbody2D rb;

	public bool IsDragging { get; set; }
	
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
			IsDragging = true;
			propDragging = CacheComponent.GetProp(collider);
			propDragging.IsDragging = true;
			Rigidbody2D rb = propDragging.rb;

			if (!rb) {
				return;
			}

			springJoint.transform.position = worldPos;
			springJoint.anchor = Vector2.zero;
			springJoint.connectedAnchor = rb.transform.InverseTransformPoint(worldPos);
			springJoint.dampingRatio = m_Damping;
			springJoint.frequency = m_Frequency;
			springJoint.enableCollision = false;
			springJoint.connectedBody = rb;
			springJoint.autoConfigureDistance = true;
			// springJoint.distance = 2f;
			

			// Fetch the collider body.
			// var body = collider.attachedRigidbody;
			// if (!body)
			// 	return;


			// Add a target joint to the Rigidbody2D GameObject.
			// m_TargetJoint = propDragging.gameObject.AddComponent<TargetJoint2D>();
			// m_TargetJoint.dampingRatio = m_Damping;
			// m_TargetJoint.frequency = m_Frequency;
			// m_TargetJoint.maxForce = 999999;
			// m_TargetJoint.autoConfigureTarget = false;
			//
			// // Attach the anchor to the local-point where we clicked.
			// m_TargetJoint.anchor = m_TargetJoint.transform.InverseTransformPoint (worldPos);

		}
		else if (Input.GetMouseButtonUp (0)) {
			IsDragging = false;
			if (propDragging) {
				propDragging.IsDragging = false;
				propDragging = null;
			}
			// Destroy (springJoint);
			springJoint.connectedBody = null;
			return;
		}

		if (springJoint.connectedBody) {
			springJoint.transform.position = worldPos;

			if (springJoint.distance > distance) {
				springJoint.autoConfigureDistance = false;
				springJoint.distance = distance;
			}
		}
		
		// Update the joint target.
		// if (m_TargetJoint)
		// {
		// 	m_TargetJoint.target = worldPos;
		//
		// 	// Draw the line between the target and the joint anchor.
		// 	if (m_DrawDragLine) {
		// 	
		// 		Debug.DrawLine (m_TargetJoint.transform.TransformPoint (m_TargetJoint.anchor), worldPos, m_Color);
		// 	}
		// }
		
	}
}