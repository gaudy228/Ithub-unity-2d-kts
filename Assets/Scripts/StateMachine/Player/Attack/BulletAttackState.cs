using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class BulletAttackState : State
{
    private BulletFactory _bulletFactory;
    private Transform _owner;
    private LayerMask _target;
    private Vector3 _dirBullet;
    private PlayerAttack _playerAttack;

    private float _timeBetweenBullet;

    private bool _canShoot;

    public int MaxCountShoot { get; private set; } = 3;

    public int CountShoot {  get; private set; }

    private CancellationTokenSource cts;

    public BulletAttackState(PlayerAttack playerAttack, Transform owner, BulletFactory bulletFactory, float timeBetweenBullet, LayerMask target, Vector3 dir)
    {
        _playerAttack = playerAttack;
        _owner = owner;
        _bulletFactory = bulletFactory;
        _timeBetweenBullet = timeBetweenBullet;
        _target = target;
        _dirBullet = dir;
    }

    public override void Enter()
    {
        _canShoot = true;
    }

    public override void Update()
    {
        if(_canShoot)
        {
            cts = new CancellationTokenSource();
            TimeBetweenBullet(cts.Token).Forget();

            _bulletFactory.Create(_owner.transform.position, _target, _dirBullet);
        }
    }

    public override void Exit()
    {
        _canShoot = false;
        cts?.Cancel();
        cts?.Dispose();
    }

    private async UniTaskVoid TimeBetweenBullet(CancellationToken token)
    {
        _canShoot = false;
        await UniTask.Delay(TimeSpan.FromSeconds(_timeBetweenBullet), cancellationToken: token);
        _canShoot = true;
    }
}
