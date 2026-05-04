using UniRx;
using UnityEngine;
using VContainer;
using System;

public class PlayerAttack : MonoBehaviour
{
    [Inject] private BulletFactory _bulletFactory;

    [Header("BulletAttack")]
    [SerializeField] private float _timeBetweenBullet;
    [SerializeField] private LayerMask _target;
    [SerializeField] private Vector3 _dirBullet;

    [Header("MeleeAttack")]
    [SerializeField] private GameObject _melleAttack;

    [Header("InvisibilityAttack")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private ReactiveProperty<AttackStateType> _currentAttackMode = new ReactiveProperty<AttackStateType>(AttackStateType.Bullet);

    private IObservable<bool> _isBulletMode;
    private IObservable<bool> _isMeleeMode;
    private IObservable<bool> _isInvisibilityMode;

    private StateMachine _stateMachine;
    private CompositeDisposable _disposables = new CompositeDisposable();

    private void Start()
    {
        _isBulletMode = _currentAttackMode.Select(mode => mode == AttackStateType.Bullet);
        _isMeleeMode = _currentAttackMode.Select(mode => mode == AttackStateType.Melle);
        _isInvisibilityMode = _currentAttackMode.Select(mode => mode == AttackStateType.Invisibility);

        var bulletAttackState = new BulletAttackState(this, transform, _bulletFactory, _timeBetweenBullet, _target, _dirBullet);
        var meleeAttackState = new MelleAttackState(this, _melleAttack);
        var invisibilityAttackState = new InvisibilityAttackState(this, _spriteRenderer);

        _stateMachine = new StateMachine();

        _stateMachine.AddState(AttackStateType.Bullet.ToString(), bulletAttackState);
        _stateMachine.AddState(AttackStateType.Melle.ToString(), meleeAttackState);
        _stateMachine.AddState(AttackStateType.Invisibility.ToString(), invisibilityAttackState);

        _stateMachine.AddTransitionFromAny(AttackStateType.Bullet.ToString(), _isBulletMode);
        _stateMachine.AddTransitionFromAny(AttackStateType.Melle.ToString(), _isMeleeMode);
        _stateMachine.AddTransitionFromAny(AttackStateType.Invisibility.ToString(), _isInvisibilityMode);

        _stateMachine.ChangeState(AttackStateType.Bullet.ToString());
    }

    public void ToggleAttackMode()
    {
        int nextMode = ((int)_currentAttackMode.Value + 1) % 3;
        _currentAttackMode.Value = (AttackStateType)nextMode;
    }

    public void Attack()
    {
        ToggleAttackMode();
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void OnDestroy()
    {
        _stateMachine?.Dispose();
        _disposables?.Dispose();
        _currentAttackMode?.Dispose();
    }
}