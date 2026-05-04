using UnityEngine;
using VContainer;

public class SpecialAttackDecoratorFactory 
{
    private GameObject _specialAttack;

    private BulletFactory _bulletFactory;
    private Transform _player;
    private LayerMask _target;
    private Vector3 _dirBullet;

    [Inject]
    public SpecialAttackDecoratorFactory(SpecialAttackGameObject specialAttackGameObject, BulletFactory bulletFactory,PlayerGameObject player, LayerMaskEnemy target, DirBullet dirBullet)
    {
        _specialAttack = specialAttackGameObject.Value;
        _bulletFactory = bulletFactory;
        _player = player.Value.transform;
        _target = target.Value;
        _dirBullet = dirBullet.Value;
    }

    public SpecialAttack CreateDecorator(SpecialDecoratorType type, SpecialAttack wrappedAttack)
    {
        return type switch
        {
            SpecialDecoratorType.LaLaLa => new BonusSpecialAttackDecorator(wrappedAttack),
            SpecialDecoratorType.BigDamage => new BigDamageSpecialAttackDecorator(wrappedAttack, _specialAttack),
            SpecialDecoratorType.Bullet => new BulletSpecialAttackDecorator(wrappedAttack, _bulletFactory,  _player, _target, _dirBullet),
            _ => wrappedAttack
        };
    }
}
