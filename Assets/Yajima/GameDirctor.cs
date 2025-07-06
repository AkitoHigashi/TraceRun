using UnityEngine;

public class GameDirctor : MonoBehaviour
{
    [Header("Meter/deltaTime"), Tooltip("１フレームで進む距離")]
    [SerializeField]
    float _meterPerFrame;
    float _distance;
    float _elapsedTime;
    int _enemyCount = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _distance = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;//経過時間を計測
        _distance = _meterPerFrame * _elapsedTime;//１フレームで進む距離×経過時間＝進んだ距離
        Debug.Log(_distance);
    }

    /// <summary>
    /// 敵を倒し数をカウントする関数
    /// 敵が死ぬときに呼び出せばいいはず
    /// </summary>
    public void EnemyDead()
    {
        _enemyCount++;
        Debug.Log(_enemyCount + "体の敵を倒した");
    }
}
