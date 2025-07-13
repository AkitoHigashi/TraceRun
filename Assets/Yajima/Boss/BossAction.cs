using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class IntervalData
{
    public enum State
    {
        Idle,
        Run,
        Walk,
        Back,
        Right,
        Left,
        NormalAttack,
        Summon,
        Rapid,
        Beam
    }

    [Header("StateName"), Tooltip("AnimatorのParameterにあるTriggerの文字列を指定")]
    public State _state;

    [Header("RandomIntervalValue")]
    [Tooltip("NormalAttack、Summon、Rapid、BeamはAnyStateからExitまでのアニメーション再生時間（少し長めのほうがいいかも）")]
    public float _leastInterval;
}

public class BossAction : MonoBehaviour
{
    [Header("IntervalData")]
    [SerializeField]
    List<IntervalData> _intervalDataList;

    [Header("MagicBullet")]
    [SerializeField]
    GameObject _magicBullet;

    [Header("Skeleton")]
    [SerializeField]
    GameObject _skeleton;

    [Header("Beam")]
    [SerializeField]
    GameObject _beam;

    [Header("MoveXRange"), Tooltip("現在位置から左右それぞれどれだけ動けるかを0以上の数で")]
    [SerializeField]
    float _right;
    [SerializeField]
    float _left;

    [Header("MoveZRange"), Tooltip("現在位置から前後それぞれどれだけ動けるかを0以上の数で")]
    [SerializeField]
    float _forward;
    [SerializeField]
    float _back;

    [Header("Speed")]
    [SerializeField]
    float _walkSpeed;
    [SerializeField]
    float _rightSpeed;
    [SerializeField]
    float _leftSpeed;
    [SerializeField]
    float _backSpeed;
    [SerializeField]
    float _runSpeed;

    [Header("HP")]
    [SerializeField]
    float _hp = 10;

    IntervalData _intervalData;
    Animator _animator;

    Vector3 _basePos;

    int _randomState;
    float _transitionStateInterval;
    float _delta;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _basePos = transform.position;//ボス戦前の動き方によっては注意が必要かも
        _transitionStateInterval = 0.0f;//登場シーンとか入れるなら増やせばIdle状態を伸ばせる
        transform.localScale = new Vector3(-1, 1, -1);//LocalScale初期化
    }

    // Update is called once per frame
    void Update()
    {
        //ランダムな時間でモーションを切り替える
        _delta += Time.deltaTime;
        if (_delta > _transitionStateInterval)
        {
            _delta = 0;
            //enumの要素数までの数字からランダム
            _randomState = Random.Range(0, _intervalDataList.Count);

            //_intervalDataに_randomState番目のIntervalDataクラスの要素を代入
            _intervalData = _intervalDataList[_randomState];

            //現在のステートに合わせた処理
            _animator.SetTrigger(_intervalData._state.ToString());
            ChangeInterval();
        }

        if (_intervalData._state != IntervalData.State.Rapid)
        {
            Vector3 pos = transform.position;
            pos.y = _basePos.y;
            transform.position = pos;
        }

        //Run状態の時の処理
        if (_intervalData._state == IntervalData.State.Run)
        {
            if ((transform.position.z - _basePos.z) * transform.localScale.z < _forward)//行動範囲を超えていなければ移動
            {
                transform.position += Vector3.forward * _runSpeed * transform.localScale.z;
            }
            else
            {
                Debug.Log(_intervalData._state.ToString() + "から切り替え");
                //超えていたら座標を調整して
                transform.position = new Vector3(transform.position.x, transform.position.y, _basePos.z + _forward * transform.localScale.z);
                //アニメーション切り替えの条件を満たすようにする
                _delta = _transitionStateInterval;
            }
        }

        //Walk
        else if (_intervalData._state == IntervalData.State.Walk)
        {
            if ((transform.position.z - _basePos.z) * transform.localScale.z < _forward)
            {
                transform.position += Vector3.forward * _walkSpeed * transform.localScale.z;
            }
            else
            {
                Debug.Log(_intervalData._state.ToString() + "から切り替え");
                transform.position = new Vector3(transform.position.x, transform.position.y, _basePos.z + _forward * transform.localScale.z);
                _delta = _transitionStateInterval;
            }
        }

        //Back
        else if (_intervalData._state == IntervalData.State.Back)
        {
            if ((transform.position.z - _basePos.z) * transform.localScale.z > -1 * _back)
            {
                transform.position += Vector3.back * _walkSpeed * transform.localScale.z;
            }
            else
            {
                Debug.Log(_intervalData._state.ToString() + "から切り替え");
                transform.position = new Vector3(transform.position.x, transform.position.y, _basePos.z + -1 * _back * transform.localScale.z);
                _delta = _transitionStateInterval;
            }
        }

        //Right
        else if (_intervalData._state == IntervalData.State.Right)
        {
            if ((transform.position.x - _basePos.x) * transform.localScale.x < _right)
            {
                transform.position += Vector3.right * _rightSpeed * transform.localScale.x;
            }
            else
            {
                Debug.Log(_intervalData._state.ToString() + "から切り替え");
                transform.position = new Vector3(_basePos.x + _right * transform.localScale.x, transform.position.y, transform.position.z);
                _delta = _transitionStateInterval;
            }
        }

        //Left
        else if (_intervalData._state == IntervalData.State.Left)
        {
            if ((transform.position.x - _basePos.x) * transform.localScale.x > -1 * _left)
            {
                transform.position += Vector3.left * _leftSpeed * transform.localScale.x;
            }
            else
            {
                Debug.Log(_intervalData._state.ToString() + "から切り替え");
                transform.position = new Vector3(_basePos.x + -1 * _left * transform.localScale.x, transform.position.y, transform.position.z);
                _delta = _transitionStateInterval;
            }
        }
    }

    /// <summary>
    /// Attack1（通常攻撃）を行う関数
    /// Attack04 animationで呼び出している
    /// </summary>
    void NormalAttack()
    {
        if (_magicBullet)
        {
            Instantiate(_magicBullet, transform.position + Vector3.forward * 1.2f * transform.localScale.z, Quaternion.identity);
            Debug.Log("NormalAttack");
        }
        else
        {
            Debug.LogWarning("MagicBulletが登録されていません");
        }
    }

    int _summonCount = 0;

    [Header("SummonPosition"), Tooltip("召喚をするボスに対する座標を３つまで指定してください")]
    [SerializeField]
    Vector3[] _summonPosition;

    /// <summary>
    /// Attack2（スケルトン召喚）を行う関数
    /// Attack03Start animationで呼び出している
    /// </summary>
    void Summon()
    {
        if (_skeleton)
        {
            //召喚位置
            Vector3[] _summonPos = { VectorMulti(_summonPosition[0], transform.localScale), VectorMulti(_summonPosition[1], transform.localScale), VectorMulti(_summonPosition[2], transform.localScale) };
            if (-1 * _left < transform.position.x + _summonPos[_summonCount].x && transform.position.x + _summonPos[_summonCount].x < _right)
            {
                Instantiate(_skeleton, transform.position + _summonPos[_summonCount], Quaternion.identity);
                Debug.Log("Summon");
            }
            else
            {
                Debug.Log("召喚失敗");
            }
            _summonCount++;
            _summonCount %= _summonPos.Length;
        }
        else
        {
            Debug.LogWarning("Skeletonが登録されていません");
        }
    }

    int _magicBulletCount = 0;

    /// <summary>
    /// Attack3（連弾攻撃）を行う関数
    /// JumpUpAttack animationで呼び出している
    /// </summary>
    void Rapid()
    {
        if (_magicBullet)
        {
            Instantiate(_magicBullet, transform.position + VectorMulti(Vector3.forward * 1.4f, transform.localScale) + VectorMulti(Vector3.right * 0.8f, transform.localScale) + VectorMulti(Vector3.down * 0.4f + new Vector3(0, 1.5f, 0), transform.localScale), Quaternion.identity);
            Debug.Log("Rapid");
        }
        else
        {
            Debug.LogWarning("MagicBulletが登録されていません");
        }
    }

    [Header("BeamPosition"), Tooltip("ビームを生成するプレイヤーに対する座標を指定してください")]
    [SerializeField]
    Vector3 _beamPosition;

    /// <summary>
    /// Attack4（ビーム照射）を行う関数
    /// Attack02Maintain animationで呼び出している
    /// </summary>
    void Beam()
    {
        if (_beam)
        {
            Vector3 _beamPos = transform.position + Vector3.right * transform.localScale.x * 0.5f;
            _beamPos += Vector3.forward * transform.localScale.z;
            Instantiate(_beam, transform.position + VectorMulti(_beamPosition, transform.localScale), _beam.transform.localRotation);
            Debug.Log("Beam");
        }
        else
        {
            Debug.LogWarning("Beamが登録されていません");
        }
    }

    /// <summary>
    /// Stateに応じてAnimation切り替えのインターバルを変える関数
    /// </summary>
    void ChangeInterval()
    {
        _transitionStateInterval = Random.Range(_intervalData._leastInterval, _intervalData._leastInterval + 0.1f);
    }

    /// <summary>
    /// Vector3の各座標同士の掛け算を行う関数
    /// </summary>
    /// <param name="a"> 掛け算を行うVector3</param>
    /// <param name="b"> 掛け算を行うVector3</param>
    /// <returns></returns>
    Vector3 VectorMulti(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
}
