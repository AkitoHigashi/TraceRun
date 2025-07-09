using UnityEngine;

public class Enemy1Facoty : IEnemyFactory
{
    private GameObject _enemy1Prefab;

    public Enemy1Facoty(GameObject prefab)
    {
        _enemy1Prefab = prefab;
    }
    public EnemyBase CreateEnemy(Vector3 spawnPosition)
    {
        GameObject enemy = Object.Instantiate(_enemy1Prefab, spawnPosition, Quaternion.identity);
        return enemy.GetComponent<EnemyBase>();
    }
}
