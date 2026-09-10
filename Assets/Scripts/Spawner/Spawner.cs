using System;
using System.Collections;
using UnityEngine;
using VContainer;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _spawnPoint;

    [Header("Настройки высоты")]
    [SerializeField] private float _minHeight = -2.5f;
    [SerializeField] private float _maxHeight = 2.5f;

    [Header("Интервал спавна")]
    [SerializeField] private float _minSpawnTime;
    [SerializeField] private float _maxSpawnTime;

    private Func<GameObject, Vector3, Quaternion, GameObject> _create;

    [Inject]
    public void Construct(Func<GameObject, Vector3, Quaternion, GameObject> create)
    {
        _create = create;
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float delay = UnityEngine.Random.Range(_minSpawnTime, _maxSpawnTime);
            yield return new WaitForSeconds(delay);

            float randomY = UnityEngine.Random.Range(_minHeight, _maxHeight);

            Vector3 position = _spawnPoint.position;
            position.y = randomY;

           _create(_prefab, position, _spawnPoint.rotation);
        }
    }
}