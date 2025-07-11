using UnityEngine;

[CreateAssetMenu(fileName = "NewBossEnemyData", menuName = "BossEnemy/Boss Enemy Data")]

public class BossEnemyData : ScriptableObject
{
    public string TypeName;
    public GameObject Prefab;
    public float MoveSpeed;
    public int Health;
    public int AttackDamage;
    public float AttackCoolTime;
}
