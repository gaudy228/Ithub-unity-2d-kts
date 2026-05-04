using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameScope : LifetimeScope
{
    [SerializeField] private Bullet _bulletPrefab;

    [SerializeField] private PlayerSpecialAttack _playerSpecialAttack;

    [SerializeField] private GameObject _specialAttack;

    [SerializeField] private GameObject _player;

    [Header("BulletSpecialAttack")]

    [SerializeField] private LayerMask _enemy;

    [SerializeField] private Vector3 _dirBullet;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<BulletFactory>(Lifetime.Singleton)
           .WithParameter(typeof(Bullet), _bulletPrefab);

        builder.RegisterInstance(_playerSpecialAttack);

        builder.Register<SpecialAttackDecoratorFactory>(Lifetime.Singleton);



        builder.RegisterInstance(new SpecialAttackGameObject { Value = _specialAttack });

        builder.RegisterInstance(new PlayerGameObject { Value = _player });

        builder.RegisterInstance(new LayerMaskEnemy { Value = _enemy });

        builder.RegisterInstance(new DirBullet { Value = _dirBullet });
    }
}

public class SpecialAttackGameObject { public GameObject Value; }

public class PlayerGameObject {  public GameObject Value; }

public class LayerMaskEnemy {  public LayerMask Value; }

public class DirBullet {  public Vector3 Value; }
