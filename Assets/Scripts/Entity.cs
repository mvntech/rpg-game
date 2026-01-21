using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;

    [Header("Attack Details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask targetLayer;

    [Header("Movement Details")]
    private float xInput;
    private float jumpForce = 15f;
    [SerializeField] protected float moveSpeed = 5f;
    private bool facingRight = true;
    private bool isAttacking = false;
    protected int facingDir = 1;

    [Header("Collision Details")]
    [SerializeField] private float groundCheckDistance;
    private bool isGrounded;
    [SerializeField] private LayerMask groundLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    protected virtual void Update()
    {
        HandleCollision();
        HandleInput();
        HandleMovement(xInput);
        HandleAnimation();
        HandleFlip();
    }
    void HandleInput()
    {
        xInput = Keyboard.current.aKey.isPressed ? -1f : Keyboard.current.dKey.isPressed ? 1f : 0f;
        if (Keyboard.current.wKey.wasPressedThisFrame)
            TryToJump();

        if(Keyboard.current.eKey.isPressed)
            AttemptToAttack();
    }
    protected virtual void HandleMovement(float direction)
    {
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
    }
    protected void HandleAnimation()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }
    protected void HandleFlip()
    {
        if (xInput > 0 && !facingRight)
            Flip();
        else if (xInput < 0 && facingRight)
            Flip();
    }
    protected virtual void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
    }
    public void DamageTargets()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, targetLayer);
        foreach (var enemy in enemies)
        {
            Entity enemyScript = enemy.GetComponent<Entity>();
            enemyScript?.TakeDamage();
        }
    }
    private void TakeDamage()
    {
        throw new NotImplementedException();
    }
    void TryToJump()
    {
        if(isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
        facingDir *= -1;
    }
    protected void AttemptToAttack()
    {
        if (isGrounded && !isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("attack");
        }
        else
        {
            isAttacking = false;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, - groundCheckDistance));
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
