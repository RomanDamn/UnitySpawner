using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private List<Vector3> _spawnersPosition;
    [SerializeField] private float _repeatRate = 2f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private int _spawnersCount = 0;
    private Random random = new Random();

    public ObjectPool<Enemy> _pool;

    public event Action<Enemy> Moved;

    private void Start()
    {
        InvokeRepeating(nameof(GetEnemy), 0.0f, _repeatRate);
        _spawnersCount = _spawnersPosition.Count;
    }

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (enemy) => OnGet(enemy),
            actionOnRelease: (enemy) => OnRelese(enemy),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void OnGet(Enemy enemy)
    {
        Vector3 spawnerPosition = GetRandomSpawnerPosition();
        enemy.transform.position = spawnerPosition;

		enemy.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

		var rotation = GetRandomRotation();
        enemy.ChangeRotation(rotation);
        enemy.gameObject.SetActive(true);
        Moved?.Invoke(enemy);
    }

	private void OnRelese(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void GetEnemy()
    {
        _pool.Get();
    }

    private Vector3 GetRandomSpawnerPosition()
    {
        int randomSpawnerIndex = random.Next(0, _spawnersCount);
        return _spawnersPosition[randomSpawnerIndex];
    }

    private Quaternion GetRandomRotation()
    {
        return Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
    }
}
