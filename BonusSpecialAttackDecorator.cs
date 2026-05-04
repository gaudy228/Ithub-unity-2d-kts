using UnityEngine;

public class BonusSpecialAttackDecorator : SpecialAttackDecorator
{
    public override void Attack()
    {
        _component.Attack();
        BonusAttack();
    }

    public override void BonusAttack()
    {
        Debug.Log("LaLaLa");
    }
}
