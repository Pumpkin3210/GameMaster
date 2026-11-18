
using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{
    public float jumpHeight = 5f;
    public float movementSpeed = 5f;
    public float checkDistance = 1f;
    public LayerMask ground;

    private bool isGrounded = false;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        rb.freezeRotation = true;
        rb.gravityScale = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,//pos
            Vector2.down,//direction
            checkDistance,//distance
            ground//layer mask
        );

        isGrounded = hit.collider != null;

        if (Input.GetKey(KeyCode.A))
        {
            Move(-1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Move(1);
        }
        else
        {
            Move(0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        }
    }

    void Move(int direction)
    {
        rb.linearVelocity = new Vector2(
            direction * movementSpeed,
            rb.linearVelocity.y
        );
    }
}
