using UnityEngine;
using VContainer;

public class Enemy : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _speed;

    private CommandInvoker _invoker;
    private ICommand _curCommand;

    [Inject]
    public void Construct(PlayerGameObject playerGameObject)
    {
        _player = playerGameObject.Value;
    }

    private void Start()
    {
        _invoker = new CommandInvoker();
    }

    public void MoveCommand()
    {
        _curCommand = new MoveEnemyCommand(_enemy, _player, _speed);

        _invoker.ExecuteCommand(_curCommand);
    }

    public void UndoLastCommand()
    {
        _invoker.UndoLastCommand();
    }
}
