using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameDirctor : MonoBehaviour
{
    [Header("PowerUpData(ScriptableObject)")]
    [SerializeField]
    PowerUpData _powerUpData;

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

    [Header("Result"), Tooltip("クリア時に表示する\"オブジェクト\"を設定")]
    [SerializeField]
    GameObject _result;

    [Header("PowerUpGroup"), Tooltip("パワーアップ時に表示するイメージの\"グループ\"を設定")]
    [SerializeField]
    GameObject _powerUpGroup;

    [Header("Meter/deltaTime"), Tooltip("１フレームで進む距離")]
    [SerializeField]
    float _meterPerFrame;

    [Header("Distance Until BossBattle"), Tooltip("ボスがいる地点")]
    [SerializeField]
    List<float> _meters;

    [Header("PowerUp Score"), Tooltip("強化要素を表示するスコアの条件（次の強化までの幅）")]
    [SerializeField]
    List<int> _powerUpScore;

    /// <summary>
    /// 基本はボタンは３つ設定するけど今後増やす可能性もある
    /// </summary>
    [Header("Button"), Tooltip("パワーアップ時に表示する\"ボタン\"を設定")]
    [SerializeField]
    List<GameObject> _button;

    /// <summary>
    /// ボタンのイメージのリストに対応するインデックスのリスト
    /// </summary>
    List<int> _powerUpIndex;

    float _elapsedTime;
    float _TimeForDistance;
    float _distance;
    int _enemyCount;
    int _previousScore;

    [Header("UICheckParameters")]
    [SerializeField]
    int _nowScore;
    [SerializeField]
    bool _isInBossBattle = false;
    [SerializeField]
    bool _clear = false;
    [SerializeField]
    bool _powerUpCondition = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //初期化
        _distance = 0.0f;
        _elapsedTime = 0.0f;
        _TimeForDistance = 0.0f;
        _enemyCount = 0;
        _previousScore = 0;
        _nowScore = 0;
        _isInBossBattle = false;
        _clear = false;
        _powerUpCondition = false;
        if (_meters == null || _meters.Count == 0)
        {
            Debug.LogWarning("到達距離のリストが空です");
        }
        else
        {
            _meters.Sort();
        }

        if (_powerUpScore == null || _powerUpScore.Count == 0)
        {
            Debug.LogWarning("パワーアップスコアのリストが空です");
        }
        else
        {
            _powerUpScore.Sort();
        }

        if (_button == null || _button.Count == 0)
        {
            Debug.LogWarning("ボタンのリストが空です");
        }

        if (!_timeText || !_distanceText || !_enemyCountText || !_scoreText)
        {
            Debug.LogWarning("いずれかのテキストが設定されていません");
        }

        if (_result == null)
        {
            Debug.LogWarning("リザルトのオブジェクトが設定されていません");
        }
        else
        {
            _result.SetActive(false);
        }

        if (_powerUpGroup == null)
        {
            Debug.LogWarning("パワーアップ時に表示するオブジェクトが設定されていません");
        }
        else
        {
            _powerUpGroup.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_clear)//クリアしていないなら
        {
            if (!_powerUpCondition)//使わない場合消す
            {
                //経過時間を計測
                _elapsedTime += Time.deltaTime;

                //１フレームで進む距離×経過時間＝進んだ距離
                if (!_isInBossBattle)
                {
                    _TimeForDistance += Time.deltaTime;
                    _distance = _meterPerFrame * _TimeForDistance;
                    //距離テキスト
                    if (_distanceText != null)
                    {
                        _distanceText.text = _distance.ToString("0000.0") + "m";
                    }
                    else
                    {
                        Debug.LogWarning("距離を表示するためのテキストがありません");
                    }
                }

                //時間テキスト
                if (_timeText != null)
                {
                    _timeText.text = _elapsedTime.ToString("0.00");
                }
                else
                {
                    Debug.LogWarning("時間を表示するためのテキストがありません");
                }

                //スコアテキスト
                if (_scoreText != null)
                {
                    _scoreText.text = _nowScore.ToString("00000");
                }
                else
                {
                    Debug.LogWarning("スコアを表示するためのテキストがありません");
                }

                //敵カウントテキスト
                if (_enemyCountText != null)
                {
                    _enemyCountText.text = _enemyCount.ToString("000");
                }
                else
                {
                    Debug.LogWarning("倒した敵の数を表示するためのテキストがありません");
                }
            }
        }
        else
        {//クリア時の処理
            if (_result == null)
            {
                Debug.LogWarning("リザルトのオブジェクトが設定されていません");
            }
            else
            {
                _result.SetActive(true);
            }
        }

        if (_distance >= _meters[0])//一定距離に達したら
        {
            _isInBossBattle = true;
            if (_meters.Count > 1)//ボス戦突入の距離が更新される
            {
                _meters.RemoveAt(0);
            }
        }

        if (_nowScore - _previousScore >= _powerUpScore[0])//一定スコアに達したら
        {
            PowerUpSelect();
        }
    }

    /// <summary>
    /// 敵を倒し数をカウントする関数
    /// 敵のオブジェクト内で呼び出す
    /// </summary>
    public void EnemyDead()
    {
        _enemyCount++;
    }

    /// <summary>
    /// スコアを加算する関数
    /// 敵のオブジェクト内で呼び出す
    /// </summary>
    /// <param name="score"> 倒した敵に応じたスコア</param>
    public void AddScore(int score)
    {
        _nowScore += score;
    }

    /// <summary>
    /// ボス戦を出た時に呼び出す関数
    /// </summary>
    public void OutBossBattle()
    {
        PowerUpSelect();
        _isInBossBattle = false;
    }

    /// <summary>
    /// ゲームを再開するための関数
    /// ボタンで呼び出す
    /// </summary>
    public void GameResume()
    {
        //_powerUpCondition = false;
        for (int i = 0; i < _button.Count; i++)
        {
            _powerUpGroup.SetActive(false);
        }
        Time.timeScale = 1;
    }

    /// <summary>
    /// クリア判定をする関数
    /// </summary>
    public void Clear()
    {
        _clear = true;
    }

    /// <summary>
    /// ボタンをアクティブにしてパワーアップをランダムに選ぶ関数
    /// </summary>
    public void PowerUpSelect()
    {
        _previousScore = _nowScore;
        _powerUpGroup.SetActive(true);
        Time.timeScale = 0;

        //_powerUpCondition = true;
        if (_powerUpScore.Count > 1)//スコアの達成条件が更新される
        {
            _powerUpScore.RemoveAt(0);
        }

        //リストを初期化
        _powerUpIndex = new List<int>();
        for (int i = 0; i < _powerUpData._list.Count; i++)
        {
            _powerUpIndex.Add(i);
        }

        //ランダムにリストの要素を選ぶ（パワーアップ要素を選ぶ）
        for (int i = 0; i < _button.Count; i++)
        {
            //ボタンに割り振るパワーアップ要素がボタンの数よりも多いとき（パワーアップ要素を選べるとき）
            if ( _powerUpData._list.Count - i > 0)
            {
                //乱数生成
                int rand = Random.Range(0, _powerUpIndex.Count);
                //ボタンに対して番号を割り振る
                _button[i].GetComponent<PowerUp>().SetImage(_powerUpIndex[rand]);
                //選ばれた要素を削除
                _powerUpIndex.RemoveAt(rand);
            }
        }
    }
}
