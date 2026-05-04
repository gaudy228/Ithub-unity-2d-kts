using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine;

public class DefaultSpecialAttack : SpecialAttack
{
    protected GameObject _specialAttack;

    protected float _timeSpecialAttack;

    protected CancellationTokenSource cts;

    public DefaultSpecialAttack(GameObject specialAttack, float timeAction)
    {
        _specialAttack = specialAttack;
        _timeSpecialAttack = timeAction;
    }

    public override void Attack()
    {
        if(cts != null)
        {
            cts.Cancel();
            cts.Dispose();
        }
        cts = new CancellationTokenSource();
        TimeSpecialAttack(cts.Token).Forget();
    }

    public async UniTaskVoid TimeSpecialAttack(CancellationToken token)
    {
        _specialAttack.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(_timeSpecialAttack), cancellationToken: token);
        _specialAttack.SetActive(false);
    }
}
