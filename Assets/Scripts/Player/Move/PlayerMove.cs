using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour, ISubject
{
    [SerializeField] private float _speed;

    private bool _isMoving = false;

    private CancellationTokenSource cts;

    private List<IObserver> observers = new List<IObserver>();

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
                Notify();
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

    public void Attach(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void Detached(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in observers)
        {
            observer.UpdateObserver();
        }
    }
}
