using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    int _enemyCount = 0;

    /// <summary>
    /// 敵を倒し数をカウントする関数
    /// 敵が死ぬときに呼び出せばいいはず
    /// </summary>
    public void EnemyDead()
    {
        _enemyCount++;
        Debug.Log(_enemyCount+"体の敵を倒した");
    }
}
