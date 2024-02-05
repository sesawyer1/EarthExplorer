
using UnityEngine;

public class Vinny_Movement : MonoBehaviour
{
    float horizontalInput;
    float moveSpeed = 5f;
    bool isFacingRight = false;
    float jumpPower = 8f;
    bool isJumping = false;
    Animator animator;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = 0f;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1;
        }

        FlipSprite();

        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJumping = true;
            animator.SetBool("isJumping", isJumping);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        animator.SetFloat("xVel", System.Math.Abs(rb.velocity.x));
        animator.SetFloat("yVel", rb.velocity.y);
    }

    void FlipSprite()
    {
        if (isFacingRight && horizontalInput > 0f || !isFacingRight && horizontalInput < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
        animator.SetBool("isJumping", isJumping);
    }
}
