using UnityEngine;

public class Enemy : Entity
{
    private bool playerDetected;
    protected override void Update()
    {
        HandleCollision();
        HandleAnimation();
        HandleMovement(facingDir);
        HandleFlip();
        HandleAttack();
    }
    protected override void HandleCollision()
    {
        base.HandleCollision();
        playerDetected = Physics2D.OverlapCircle(attackPoint.position, attackRadius, targetLayer);
    }
    protected override void HandleAttack()
    {
        if(playerDetected && !isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("attack");
        }
    }
}
