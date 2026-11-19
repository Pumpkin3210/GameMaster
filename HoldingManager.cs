using UnityEngine;

public class HoldingManager : MonoBehaviour
{
    public GameObject targetObject;
    public float angle = 0f;
    public float distanceFromOrigin;
    public Vector2 mousePos;
    public Vector2 direction;
    private float angleOffset;
    private Vector2 offset;

    void Start()
    {
        if (targetObject != null)
        {
            offset = (Vector2)(targetObject.transform.position - transform.position);
            angleOffset = targetObject.transform.eulerAngles.z; // Fixed to use eulerAngles for degrees
            distanceFromOrigin = GetDistance(transform.position, targetObject.transform.position);
        }
    }

    void Update()
    {
        if (targetObject == null) return;

        Vector3 screenPos = Input.mousePosition;
        screenPos.z = Mathf.Abs(Camera.main.transform.position.z);
        mousePos = Camera.main.ScreenToWorldPoint(screenPos);

        Vector2 originPosition = transform.position;
        direction = (mousePos - originPosition).normalized;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector2 newPos = originPosition + (direction * distanceFromOrigin);
        targetObject.transform.position = new Vector3(newPos.x, newPos.y, targetObject.transform.position.z);

        targetObject.transform.rotation = Quaternion.Euler(0f, 0f, angle + angleOffset);
    }
    public float GetDistance(Vector2 pointA, Vector2 pointB)
    {
        float dx = pointB.x - pointA.x;
        float dy = pointB.y - pointA.y;
        return Mathf.Sqrt(dx * dx + dy * dy);
    }
}