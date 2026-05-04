
public abstract class SpecialAttackDecorator : SpecialAttack
{
    protected SpecialAttack _component;

    public SpecialAttackDecorator(SpecialAttack component)
    {
        _component = component;
    }

    public override void Attack()
    {
        _component.Attack();
    }

    public abstract void BonusAttack();
}
