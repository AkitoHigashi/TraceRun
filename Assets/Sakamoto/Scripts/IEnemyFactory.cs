using UnityEngine;

public interface IEnemyFactory
{
    EnemyBase CreateEnemy(Vector3 spawnPosition);
}