using UnityEngine;

public class ObserverManager : MonoBehaviour
{
    [Header("Subject")]
    [SerializeField] private PlayerMove _playerMove;

    [Header("Observers")]
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private ReloadMoveVisual _reloadMoveVisual;
    [SerializeField] private PlayerMoveSound _playerMoveSound;

    private void Start()
    {
        if(_playerMove != null)
        {
            if(_enemyManager != null)
            {
                _playerMove.Attach(_enemyManager);
            }
            if(_reloadMoveVisual != null)
            {
                _playerMove.Attach(_reloadMoveVisual);
            }
            if(_playerMoveSound != null)
            {
                _playerMove.Attach(_playerMoveSound);
            }
        }
    }

    private void OnDestroy()
    {
        if (_playerMove != null)
        {
            if (_enemyManager != null)
            {
                _playerMove.Detached(_enemyManager);
            }
            if (_reloadMoveVisual != null)
            {
                _playerMove.Detached(_reloadMoveVisual);
            }
            if (_playerMoveSound != null)
            {
                _playerMove.Detached(_playerMoveSound);
            }
        }
    }
}
