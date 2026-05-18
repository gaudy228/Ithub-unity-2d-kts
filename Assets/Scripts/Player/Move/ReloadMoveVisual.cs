using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ReloadMoveVisual : MonoBehaviour, IObserver
{
    [SerializeField] private PlayerMove _playerMove;

    [SerializeField] private Image _reloadBar;

    private CancellationTokenSource cts;

    private void Reload()
    {
        if(cts != null)
        {
            cts.Cancel();
        }
        cts = new CancellationTokenSource();
        TimeForMoving(cts.Token).Forget();
    }

    private async UniTask TimeForMoving(CancellationToken token)
    {
        _reloadBar.fillAmount = 1f;

        _reloadBar.DOFillAmount(0f, _playerMove.TimeMoving).SetEase(Ease.Linear);

        await UniTask.Delay(TimeSpan.FromSeconds(_playerMove.TimeMoving), cancellationToken: token);
    }

    public void UpdateObserver()
    {
        Reload();
    }
}
