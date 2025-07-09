using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyDatabase", menuName = "Enemy/Enemy Database")]
public class EnemyDatabase : ScriptableObject
{
    public List<EnemyData> enemyList;
}