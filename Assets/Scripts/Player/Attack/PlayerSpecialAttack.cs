using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class PlayerSpecialAttack : MonoBehaviour
{
    private SpecialAttack _specialAttack;

    [Inject] private SpecialAttackDecoratorFactory _specialAttackDecoratorFactory;

    [SerializeField] private GameObject _specialAttackGameObject;

    [SerializeField] private float _timeAction;

    private List<SpecialDecoratorType> _activeDecorators = new List<SpecialDecoratorType>();

    public event Action OnSpecialAttack;

    private void Start()
    {
        ResetDecorators();
    }

    public void ResetDecorators()
    {
        _specialAttack = new DefaultSpecialAttack(_specialAttackGameObject, _timeAction);
        _activeDecorators.Clear();
    }

    public void AddDecorator(SpecialDecoratorType type)
    {
        _specialAttack = _specialAttackDecoratorFactory.CreateDecorator(type, _specialAttack);
        _activeDecorators.Add(type);
    }

    public void Attack()
    {
        _specialAttack.Attack();
        OnSpecialAttack?.Invoke();
    }
}
