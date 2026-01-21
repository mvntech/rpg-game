using UnityEngine;

public class Enemy : Entity
{
    protected override void Update()
    {
        HandleCollision();
        HandleAnimation();
        HandleMovement(facingDir);
        HandleFlip();
    }
}
