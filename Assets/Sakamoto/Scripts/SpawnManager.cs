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
        string[] types = { "enemy1", "enemy2", "enemy3" };
        string chosenType = types[Random.Range(0, types.Length)];

        // 最小位置と最大位置の間でランダムに選択
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