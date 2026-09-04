using UnityEngine;
using VContainer;

public class CoinDeath : CreatureDeath
{
    [Inject] private PlayerData playerData;

    [SerializeField] private LayerMask _playerMask;

    private const int _addScore = 1;

    public override void Trigger(Collider2D collision)
    {
        if (LayerMaskUtil.ContainsLayer(_deathMask, collision.gameObject))
        {
            if(LayerMaskUtil.ContainsLayer(_playerMask, collision.gameObject))
            {
                AddScore();
            }
            Death();
        }
    }

    private void AddScore()
    {
        playerData.AddScore(_addScore);
    }

    public override void Death()
    {
        Destroy(gameObject);
    }
}
