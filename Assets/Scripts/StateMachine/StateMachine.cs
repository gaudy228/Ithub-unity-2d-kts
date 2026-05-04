using System;
using System.Collections.Generic;
using UniRx;

public class StateMachine
{
    private State _currentState;
    private CompositeDisposable _disposables = new CompositeDisposable();

    private Dictionary<string, State> _states = new Dictionary<string, State>();

    public void AddState(string name, State state)
    {
        _states[name] = state;
    }

    public void AddTransition(string from, string to, IObservable<bool> condition)
    {
        condition
            .Where(c => c == true)
            .Subscribe(_ =>
            {
                if (_currentState == _states[from])
                {
                    ChangeState(to);
                }
            })
            .AddTo(_disposables);
    }

    public void AddTransitionFromAny(string to, IObservable<bool> condition)
    {
        condition
            .Where(c => c == true)
            .Subscribe(_ =>
            {
                ChangeState(to);
            })
            .AddTo(_disposables);
    }

    public void ChangeState(string stateName)
    {
        _currentState?.Exit();

        _currentState = _states[stateName];

        _currentState.Enter();
    }

    public void Update()
    {
        _currentState?.Update();
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}
