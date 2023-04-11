using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 5f;

    public float moveSpeed = 2f;

    public KeyCode jumpKey = KeyCode.Space;

    public LayerMask groundLayer;

    private Rigidbody2D rb;

    private bool isGrounded;

    private float groundCheckRadius = 0.2f;

    public Transform groundCheckPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded =
            Physics2D
                .OverlapCircle(groundCheckPoint.position,
                groundCheckRadius,
                groundLayer);

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void Move()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }
}
