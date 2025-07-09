using UnityEngine;

public class Enemy1 : EnemyBase
{
    private void Update()
    {
        MoveToBase();
    }

    public override void Attack()
    {
        // 攻撃ロジック（未実装）
    }
}
