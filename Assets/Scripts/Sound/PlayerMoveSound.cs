using UnityEngine;

public class PlayerMoveSound : MonoBehaviour, IObserver
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _moveClip;

    public void UpdateObserver()
    {
        _audioSource.clip = _moveClip;
        _audioSource.Play();
    }
}
