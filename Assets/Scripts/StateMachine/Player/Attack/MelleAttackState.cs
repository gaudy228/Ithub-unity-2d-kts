using UnityEngine;

public class MelleAttackState : State
{
    private PlayerAttack _playerAttack;

    private GameObject _melleAttack;

    public MelleAttackState(PlayerAttack playerAttack, GameObject melleAttack)
    {
        _playerAttack = playerAttack;
        _melleAttack = melleAttack;
    }

    public override void Enter()
    {
        _melleAttack.SetActive(true);
    }

    public override void Exit()
    {
        _melleAttack.SetActive(false);
    }
}
