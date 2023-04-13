using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 5f;

    public float moveSpeed = 2f;

    public KeyCode jumpKey = KeyCode.Space;

    public LayerMask groundLayer;

    private Rigidbody2D rb;

    public Animator anim;

    private bool isGrounded;

    private float groundCheckRadius = 0.5f;

    public Transform groundCheckPoint;

    public bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDead)
        {
            isGrounded =
                Physics2D
                    .OverlapCircle(groundCheckPoint.position,
                    groundCheckRadius,
                    groundLayer);

            anim.SetBool("isGrounded", isGrounded);

            if (Input.GetKeyDown(jumpKey) && isGrounded)
            {
                anim.SetBool("isGrounded", false);
                foreach (SwitchController
                    switchController
                    in
                    FindObjectsOfType<SwitchController>()
                )
                {
                    if (switchController.isNearPlayer())
                    {
                        switchController.ActivateSwitch();
                        break;
                    }
                }
                Jump();
            }
        }
    }

    void FixedUpdate()
    {
        if (!isDead) Move();
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
