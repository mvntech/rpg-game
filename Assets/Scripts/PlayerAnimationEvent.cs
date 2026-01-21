using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private Entity player;
    void Awake()
    {
        player = GetComponentInParent<Entity>();
    }
    //public void DamageEnemies() => player.DamageEnemies();
}
