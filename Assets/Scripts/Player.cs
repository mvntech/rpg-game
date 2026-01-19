using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private float xInput;
    private float jumpForce = 5f;
    private float moveSpeed = 5f;
    private bool facingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleAnimation();
        HandleFlip();
    }
    void HandleInput()
    {
        xInput = Keyboard.current.aKey.isPressed ? -1f : Keyboard.current.dKey.isPressed ? 1f : 0f;
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            Jump();
    }
    void HandleMovement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }
    void HandleAnimation()
    {
        bool isMoving = rb.linearVelocity.x != 0;
        anim.SetBool("isMoving", isMoving);
    }
    void HandleFlip()
    {
        if (xInput > 0 && !facingRight)
            Flip();
        else if (xInput < 0 && facingRight)
            Flip();
    }
    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }
}
