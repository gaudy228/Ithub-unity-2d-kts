using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IObserver
{
    [SerializeField] private PlayerMove _playerMove;

    [SerializeField] private PlayerSpecialAttack _playerSpecialAttack;

    [SerializeField] private List<Enemy> _enemyList = new List<Enemy>();

    private void OnEnable()
    {
        _playerSpecialAttack.OnSpecialAttack += UndoEnemy;
    }

    private void OnDisable()
    {
        _playerSpecialAttack.OnSpecialAttack -= UndoEnemy;
    }

    private void EnemiesMove()
    {
        foreach (var enemy in _enemyList)
        {
            enemy.MoveCommand();
        }
    }

    private void UndoEnemy()
    {
        foreach (var enemy in _enemyList)
        {
            enemy.UndoLastCommand();
        }
    }

    public void UpdateObserver()
    {
        EnemiesMove();
    }
}
