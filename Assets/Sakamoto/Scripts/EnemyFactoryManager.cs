using System.Collections.Generic;
using UnityEngine;

public class EnemyFactoryManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy1Prefab;
    [SerializeField] private GameObject _enemy2Prefab;
    [SerializeField] private GameObject _enemy3Prefab;

    private Dictionary<string, IEnemyFactory> factories = new Dictionary<string, IEnemyFactory>();
    private void Awake()
    {
        factories["enemy1"] = new Enemy1Facoty(_enemy1Prefab);
        factories["enemy2"] = new Enemy2Facoty(_enemy2Prefab);
        factories["enemy3"] = new Enemy3Facoty(_enemy3Prefab);

    }

    public EnemyBase CreateEnemy(string type, Vector3 spawnPos)
    {
        if (factories.TryGetValue(type, out var factory))
        {
            return factory.CreateEnemy(spawnPos);
        }

        Debug.LogError($"Factory for type {type} not foun!");
        return null;
    }
}
