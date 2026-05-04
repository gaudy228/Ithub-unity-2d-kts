using UnityEngine;

public class MoveEnemyCommand : ICommand
{
    private GameObject _enemy;
    private GameObject _player;
    private float _speed;
    private Vector3 _previousPosition;

    public MoveEnemyCommand(GameObject enemy, GameObject player, float speed)
    {
        _enemy = enemy;
        _player = player;
        _speed = speed;
    }

    public void Execute()
    {
        _previousPosition = _enemy.transform.position;

        Vector3 direction = (_player.transform.position - _enemy.transform.position).normalized;

        _enemy.transform.position += direction * _speed;
    }

    public void Undo()
    {
        _enemy.transform.position = _previousPosition;
    }
}
