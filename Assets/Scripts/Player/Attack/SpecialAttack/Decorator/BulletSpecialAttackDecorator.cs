using UnityEngine;

public class BulletSpecialAttackDecorator : SpecialAttackDecorator
{
    private BulletFactory _bulletFactory;
    private Transform _player;
    private LayerMask _target;
    private Vector3 _dirBullet;

    public BulletSpecialAttackDecorator(SpecialAttack component, BulletFactory bulletFactory, Transform player, LayerMask target, Vector3 dirBullet) : base(component)
    {
        _bulletFactory = bulletFactory;
        _player = player;
        _target = target;
        _dirBullet = dirBullet;
    }

    public override void Attack()
    {
        _component.Attack();
        BonusAttack();
    }

    public override void BonusAttack()
    {
        _bulletFactory.Create(_player.position, _target, _dirBullet);
    }
}
