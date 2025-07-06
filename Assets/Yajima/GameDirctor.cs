using UnityEngine;
using UnityEngine.UI;

public class GameDirctor : MonoBehaviour
{
    [Header("Time Text"), Tooltip("時間を表示するテキストオブジェクト")]
    [SerializeField]
    Text _timeText;

    [Header("Score Text"), Tooltip("スコアを表示するテキストオブジェクト")]
    [SerializeField]
    Text _scoreText;

    [Header("Distance Text"), Tooltip("走った距離を表示するテキストオブジェクト")]
    [SerializeField]
    Text _distanceText;

    [Header("Enemy_Count Text"), Tooltip("倒した敵の数を表示するテキストオブジェクト")]
    [SerializeField]
    Text _enemyCountText;

    [Header("Meter/deltaTime"), Tooltip("１フレームで進む距離")]
    [SerializeField]
    float _meterPerFrame;

    float _elapsedTime;
    float _distance;
    int _enemyCount;
    int _score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _distance = 0.0f;
        _elapsedTime = 0.0f;
        _enemyCount = 0;
        _score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //経過時間を計測
        _elapsedTime += Time.deltaTime;

        //１フレームで進む距離×経過時間＝進んだ距離
        _distance = _meterPerFrame * _elapsedTime;

        //時間テキスト
        _timeText.text = _elapsedTime.ToString("0.00");

        //スコアテキスト
        _scoreText.text = _score.ToString("00000");

        //距離テキスト
        _distanceText.text = _distance.ToString("0000.0")+"m";

        //敵カウントテキスト
        _enemyCountText.text = _enemyCount.ToString("000");
    }

    /// <summary>
    /// 敵を倒し数をカウントする関数
    /// 敵が死ぬときに呼び出せばいいはず
    /// </summary>
    public void EnemyDead()
    {
        _enemyCount++;
    }

    /// <summary>
    /// スコアを加算する関数
    /// </summary>
    /// <param name="score"> 倒した敵に応じたスコア</param>
    public void AddScore(int score)
    {
        _score += score;
    }
}
