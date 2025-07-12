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

    [Header("StateName"), Tooltip("Animator��Parameter�ɂ���Trigger�̕�������w��")]
    public State _state;

    [Header("RandomIntervalValue")]
    [Tooltip("NormalAttack�ASummon�ARapid�ABeam��AnyState����Exit�܂ł̃A�j���[�V�����Đ����ԁi�������߂̂ق������������j")]
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

    [Header("MoveXRange"), Tooltip("���݈ʒu���獶�E���ꂼ��ǂꂾ�������邩��0�ȏ�̐���")]
    [SerializeField]
    float _right;
    [SerializeField]
    float _left;

    [Header("MoveZRange"), Tooltip("���݈ʒu����O�セ�ꂼ��ǂꂾ�������邩��0�ȏ�̐���")]
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
        _basePos = transform.position;//�{�X��O�̓������ɂ���Ă͒��ӂ��K�v����
        _transitionStateInterval = 0.0f;//�o��V�[���Ƃ������Ȃ瑝�₹��Idle��Ԃ�L�΂���
        transform.localScale = new Vector3(-1, 1, -1);//LocalScale������
    }

    // Update is called once per frame
    void Update()
    {
        //�����_���Ȏ��ԂŃ��[�V������؂�ւ���
        _delta += Time.deltaTime;
        if (_delta > _transitionStateInterval)
        {
            _delta = 0;
            //enum�̗v�f���܂ł̐������烉���_��
            _randomState = Random.Range(0, _intervalDataList.Count);

            //_intervalData��_randomState�Ԗڂ�IntervalData�N���X�̗v�f����
            _intervalData = _intervalDataList[_randomState];

            //���݂̃X�e�[�g�ɍ��킹������
            _animator.SetTrigger(_intervalData._state.ToString());
            ChangeInterval();
        }

        if (_intervalData._state != IntervalData.State.Rapid)
        {
            Vector3 pos = transform.position;
            pos.y = _basePos.y;
            transform.position = pos;
        }

        //Run��Ԃ̎��̏���
        if (_intervalData._state == IntervalData.State.Run)
        {
            if ((transform.position.z - _basePos.z) * transform.localScale.z < _forward)//�s���͈͂𒴂��Ă��Ȃ���Έړ�
            {
                transform.position += Vector3.forward * _runSpeed * transform.localScale.z;
            }
            else
            {
                Debug.Log(_intervalData._state.ToString() + "����؂�ւ�");
                //�����Ă�������W�𒲐�����
                transform.position = new Vector3(transform.position.x, transform.position.y, _basePos.z + _forward * transform.localScale.z);
                //�A�j���[�V�����؂�ւ��̏����𖞂����悤�ɂ���
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
                Debug.Log(_intervalData._state.ToString() + "����؂�ւ�");
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
                Debug.Log(_intervalData._state.ToString() + "����؂�ւ�");
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
                Debug.Log(_intervalData._state.ToString() + "����؂�ւ�");
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
                Debug.Log(_intervalData._state.ToString() + "����؂�ւ�");
                transform.position = new Vector3(_basePos.x + -1 * _left * transform.localScale.x, transform.position.y, transform.position.z);
                _delta = _transitionStateInterval;
            }
        }
    }

    /// <summary>
    /// Attack1�i�ʏ�U���j���s���֐�
    /// Attack04 animation�ŌĂяo���Ă���
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
            Debug.LogWarning("MagicBullet���o�^����Ă��܂���");
        }
    }

    int _summonCount = 0;

    [Header("SummonPosition"), Tooltip("����������{�X�ɑ΂�����W���R�܂Ŏw�肵�Ă�������")]
    [SerializeField]
    Vector3[] _summonPosition;

    /// <summary>
    /// Attack2�i�X�P���g�������j���s���֐�
    /// Attack03Start animation�ŌĂяo���Ă���
    /// </summary>
    void Summon()
    {
        if (_skeleton)
        {
            //�����ʒu
            Vector3[] _summonPos = { VectorMulti(_summonPosition[0], transform.localScale), VectorMulti(_summonPosition[1], transform.localScale), VectorMulti(_summonPosition[2], transform.localScale) };
            if (-1 * _left < transform.position.x + _summonPos[_summonCount].x && transform.position.x + _summonPos[_summonCount].x < _right)
            {
                Instantiate(_skeleton, transform.position + _summonPos[_summonCount], Quaternion.identity);
                Debug.Log("Summon");
            }
            else
            {
                Debug.Log("�������s");
            }
            _summonCount++;
            _summonCount %= _summonPos.Length;
        }
        else
        {
            Debug.LogWarning("Skeleton���o�^����Ă��܂���");
        }
    }

    int _magicBulletCount = 0;

    /// <summary>
    /// Attack3�i�A�e�U���j���s���֐�
    /// JumpUpAttack animation�ŌĂяo���Ă���
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
            Debug.LogWarning("MagicBullet���o�^����Ă��܂���");
        }
    }

    [Header("BeamPosition"), Tooltip("�r�[���𐶐�����v���C���[�ɑ΂�����W���w�肵�Ă�������")]
    [SerializeField]
    Vector3 _beamPosition;

    /// <summary>
    /// Attack4�i�r�[���Ǝˁj���s���֐�
    /// Attack02Maintain animation�ŌĂяo���Ă���
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
            Debug.LogWarning("Beam���o�^����Ă��܂���");
        }
    }

    /// <summary>
    /// State�ɉ�����Animation�؂�ւ��̃C���^�[�o����ς���֐�
    /// </summary>
    void ChangeInterval()
    {
        _transitionStateInterval = Random.Range(_intervalData._leastInterval, _intervalData._leastInterval + 0.1f);
    }

    /// <summary>
    /// Vector3�̊e���W���m�̊|���Z���s���֐�
    /// </summary>
    /// <param name="a"> �|���Z���s��Vector3</param>
    /// <param name="b"> �|���Z���s��Vector3</param>
    /// <returns></returns>
    Vector3 VectorMulti(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
}
