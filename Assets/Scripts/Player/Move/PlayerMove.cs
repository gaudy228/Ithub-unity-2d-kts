using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;

    private bool _isMoving = false;

    private CancellationTokenSource cts;

    public event Action OnMove;
    [field: SerializeField] public float TimeMoving {  get; private set; }

    public void Move(Vector2 direction)
    {
        if(direction.magnitude > 0)
        {
            if (CanMove())
            {
                Vector3 move = new Vector3(direction.x, direction.y, 0) * _speed;
                transform.position += move;
                cts = new CancellationTokenSource();
                TimeForMoving(cts.Token).Forget();
                OnMove?.Invoke();
            }
        }
    }

    public bool CanMove()
    {
        return !_isMoving;
    }

    private async UniTaskVoid TimeForMoving(CancellationToken token)
    {
        _isMoving = true;
        await UniTask.Delay(TimeSpan.FromSeconds(TimeMoving), cancellationToken: token);
        _isMoving = false;
    }

    private void OnDestroy()
    {
        cts?.Cancel();
        cts?.Dispose();
    }
}
