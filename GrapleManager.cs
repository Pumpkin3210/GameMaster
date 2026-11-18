using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GrapleManager : MonoBehaviour
{
    [Header("Grapple Settings")]
    public float maxGrappleDistance = 15f;
    public LayerMask grappleLayer; // Set which layers can be grappled in the Inspector
    public float springFrequency = 2f;
    public float springDamping = 0.5f;
    public Transform grappleStartPoint;

    private SpringJoint2D joint;
    private LineRenderer lineRenderer;
    private Vector2 grapplePoint;
    private bool isGrappling = false;

    void Start()
    {
        // Note: Use gameObject.AddComponent<T>() in Unity C#
        joint = GetComponent<SpringJoint2D>();
        if (joint == null)
        {
            joint = gameObject.AddComponent<SpringJoint2D>();
        }

        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Configure defaults and disable both until Grapple() is called
        joint.enabled = false;
        joint.autoConfigureDistance = false;
        joint.frequency = springFrequency;
        joint.dampingRatio = springDamping;

        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        // Keep the grapple rope attached between the player and the anchor point
        if (isGrappling)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grapplePoint);
        }
    }

    public void Grapple()
    {
        // 1. Get mouse position in 2D world space
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(screenPos);

        // 2. Raycast from this object toward the mouse
        Vector2 origin = grappleStartPoint.position;
        Vector2 direction = (mouseWorldPos - origin).normalized;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxGrappleDistance, grappleLayer);

        // 3. If we hit a valid grapple surface, attach the joint and draw the rope
        if (hit.collider != null)
        {
            isGrappling = true;
            grapplePoint = hit.point;

            joint.connectedAnchor = grapplePoint;
            joint.distance = Vector2.Distance(origin, grapplePoint) * 0.8f; // 0.8f gives a slight initial pull
            joint.enabled = true;

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, grapplePoint);
        }
    }

    public void ReleaseGrapple()
    {
        isGrappling = false;
        joint.enabled = false;
        lineRenderer.enabled = false;
    }
}