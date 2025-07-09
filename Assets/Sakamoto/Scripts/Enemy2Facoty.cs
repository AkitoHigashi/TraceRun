using UnityEngine;

public class Enemy2Facoty : IEnemyFactory
{
    private GameObject _enemy2Prefab;

    public Enemy2Facoty(GameObject prefab)
    {
        _enemy2Prefab = prefab;
    }
    public EnemyBase CreateEnemy(Vector3 spawnPosition)
    {
        GameObject enemy = Object.Instantiate(_enemy2Prefab, spawnPosition, _enemy2Prefab.transform.rotation);
        return enemy.GetComponent<EnemyBase>();
    }
}
