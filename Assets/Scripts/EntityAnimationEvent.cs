using UnityEngine;

public class EntityAnimationEvent : MonoBehaviour
{
    private Entity entity;
    void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }
    public void DamageTargets() => entity.DamageTargets();
}
