using UnityEngine;

public class BigDamageSpecialAttackDecorator : SpecialAttackDecorator
{
    private const float _multScale = 1.1f;

    protected GameObject _specialAttack;

    private Vector3 _startScale;
    public BigDamageSpecialAttackDecorator(SpecialAttack component, GameObject specialAttack) : base(component)
    {
        _specialAttack = specialAttack;
        _startScale = _specialAttack.transform.localScale;
    }

    public override void Attack()
    {
        _component.Attack();
        BonusAttack();
    }

    public override void BonusAttack()
    {
        _specialAttack.transform.localScale = _startScale;
        _specialAttack.transform.localScale *= _multScale;
    }
}
