using UnityEngine;

public class Enemy3Facoty : IEnemyFactory
{
    private GameObject _enemy3Prefab;

    public Enemy3Facoty(GameObject prefab)
    {
        _enemy3Prefab = prefab;
    }
    public EnemyBase CreateEnemy(Vector3 spawnPosition)
    {
        GameObject enemy = Object.Instantiate(_enemy3Prefab, spawnPosition, Quaternion.identity);
        return enemy.GetComponent<EnemyBase>();
    }
}
