using UnityEngine;

public abstract class CreatureDeath : MonoBehaviour, IDeatheble
{
    [SerializeField] protected LayerMask _deathMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Trigger(collision);
    }

    public virtual void Trigger(Collider2D collision)
    {
        if (LayerMaskUtil.ContainsLayer(_deathMask, collision.gameObject))
        {
            Death();
        }
    }

    public abstract void Death();
}