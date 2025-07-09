using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private EnemyFactoryManager factoryManager;
    [SerializeField] private float spawnInterval = 2.0f;

    [Header("Spawn Position Settings")]
    [SerializeField] private Vector3 minSpawnPosition = new Vector3(-5, 0, -10);
    [SerializeField] private Vector3 maxSpawnPosition = new Vector3(5, 0, -10);

    private float _timer;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > spawnInterval)
        {
            SpawnRandomEnemy();
            _timer = 0;
        }
    }

    private void SpawnRandomEnemy()
    {
        List<string> types = factoryManager.GetAvailableTypes();
        if (types.Count == 0) return;

        string chosenType = types[Random.Range(0, types.Count)];

        Vector3 spawnPosition = new Vector3(
            Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
            Random.Range(minSpawnPosition.y, maxSpawnPosition.y),
            Random.Range(minSpawnPosition.z, maxSpawnPosition.z)
        );

        EnemyBase enemy = factoryManager.CreateEnemy(chosenType, spawnPosition);
        if (enemy != null)
        {
            enemy.MoveToBase();
        }
    }
}