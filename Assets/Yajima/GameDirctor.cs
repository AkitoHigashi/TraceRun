using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameDirctor : MonoBehaviour
{
    [Header("Time Text"), Tooltip("���Ԃ�\������e�L�X�g�I�u�W�F�N�g")]
    [SerializeField]
    Text _timeText;

    [Header("Score Text"), Tooltip("�X�R�A��\������e�L�X�g�I�u�W�F�N�g")]
    [SerializeField]
    Text _scoreText;

    [Header("Distance Text"), Tooltip("������������\������e�L�X�g�I�u�W�F�N�g")]
    [SerializeField]
    Text _distanceText;

    [Header("Enemy_Count Text"), Tooltip("�|�����G�̐���\������e�L�X�g�I�u�W�F�N�g")]
    [SerializeField]
    Text _enemyCountText;

    [Header("Meter/deltaTime"), Tooltip("�P�t���[���Ői�ދ���")]
    [SerializeField]
    float _meterPerFrame;

    [Header("Distance Until BossBattle"), Tooltip("�{�X������n�_")]
    [SerializeField]
    List<float> _meters;

    [Header("PowerUp Score"), Tooltip("�����v�f��\������X�R�A�̏����i���̋����܂ł̕��j")]
    [SerializeField]
    List<int> _powerUpScore;

    /// <summary>
    /// ��{�̓{�^���͂R�ݒ肷�邯�Ǎ��㑝�₷�\��������
    /// </summary>
    [Header("Button"), Tooltip("�p���[�A�b�v���ɕ\������{�^����ݒ�")]
    [SerializeField]
    List<GameObject> _button;

    /// <summary>
    /// �{�^���̃C���[�W�̃��X�g�ɑΉ�����C���f�b�N�X�̃��X�g
    /// </summary>
    List<int> _powerUpIndex;

    float _elapsedTime;
    float _TimeForDistance;
    float _distance;
    int _enemyCount;
    int _previousScore;
    
    [Header("PowerUpNumber"), Tooltip("�p���[�A�b�v�̎�ސ������")]
    [SerializeField]
    int _powerUpNum;

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
        //������
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
            Debug.LogWarning("���B�����̃��X�g����ł�");
        }
        else
        {
            _meters.Sort();
        }

        if (_powerUpScore == null || _powerUpScore.Count == 0)
        {
            Debug.LogWarning("�p���[�A�b�v�X�R�A�̃��X�g����ł�");
        }
        else
        {
            _powerUpScore.Sort();
        }

        if (_button == null || _button.Count == 0)
        {
            Debug.LogWarning("�{�^���̃��X�g����ł�");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_clear)//�N���A���Ă��Ȃ��Ȃ�
        {
            if (!_powerUpCondition)//�g��Ȃ��ꍇ����
            {
                //�o�ߎ��Ԃ��v��
                _elapsedTime += Time.deltaTime;

                //�P�t���[���Ői�ދ����~�o�ߎ��ԁ��i�񂾋���
                if (!_isInBossBattle)
                {
                    _TimeForDistance += Time.deltaTime;
                    _distance = _meterPerFrame * _TimeForDistance;
                    //�����e�L�X�g
                    if (_distanceText != null)
                    {
                        _distanceText.text = _distance.ToString("0000.0") + "m";
                    }
                    else
                    {
                        Debug.LogWarning("������\�����邽�߂̃e�L�X�g������܂���");
                    }
                }

                //���ԃe�L�X�g
                if (_timeText != null)
                {
                    _timeText.text = _elapsedTime.ToString("0.00");
                }
                else
                {
                    Debug.LogWarning("���Ԃ�\�����邽�߂̃e�L�X�g������܂���");
                }

                //�X�R�A�e�L�X�g
                if (_scoreText != null)
                {
                    _scoreText.text = _nowScore.ToString("00000");
                }
                else
                {
                    Debug.LogWarning("�X�R�A��\�����邽�߂̃e�L�X�g������܂���");
                }

                //�G�J�E���g�e�L�X�g
                if (_enemyCountText != null)
                {
                    _enemyCountText.text = _enemyCount.ToString("000");
                }
                else
                {
                    Debug.LogWarning("�|�����G�̐���\�����邽�߂̃e�L�X�g������܂���");
                }
            }
        }
        else
        {

        }

        if (_distance >= _meters[0])//��苗���ɒB������
        {
            _isInBossBattle = true;
            if (_meters.Count > 1)//�{�X��˓��̋������X�V�����
            {
                _meters.RemoveAt(0);
            }
        }

        if (_nowScore - _previousScore >= _powerUpScore[0])//���X�R�A�ɒB������
        {
            _previousScore = _nowScore;
            ButtonActive();
            Time.timeScale = 0;
            //_powerUpCondition = true;
            if (_powerUpScore.Count > 1)//�X�R�A�̒B���������X�V�����
            {
                _powerUpScore.RemoveAt(0);
            }
        }
    }

    /// <summary>
    /// �G��|�������J�E���g����֐�
    /// �G�̃I�u�W�F�N�g���ŌĂяo��
    /// </summary>
    public void EnemyDead()
    {
        _enemyCount++;
    }

    /// <summary>
    /// �X�R�A�����Z����֐�
    /// �G�̃I�u�W�F�N�g���ŌĂяo��
    /// </summary>
    /// <param name="score"> �|�����G�ɉ������X�R�A</param>
    public void AddScore(int score)
    {
        _nowScore += score;
    }

    /// <summary>
    /// �{�X����o�����ɌĂяo���֐�
    /// </summary>
    public void OutBossBattle()
    {
        _isInBossBattle = false;
    }

    /// <summary>
    /// �Q�[�����ĊJ���邽�߂̊֐�
    /// </summary>
    public void GameResume()
    {
        //_powerUpCondition = false;
        for (int i = 0; i < _button.Count; i++)
        {
            _button[i].SetActive(false);
        }
        Time.timeScale = 1;
    }

    /// <summary>
    /// �N���A���������֐�
    /// </summary>
    public void Clear()
    {
        _clear = true;
    }

    /// <summary>
    /// �{�^�����A�N�e�B�u�ɂ��ăp���[�A�b�v�������_���ɑI�Ԋ֐�
    /// </summary>
    public void ButtonActive()
    {
        //���X�g��������
        _powerUpIndex = new List<int>();
        for (int i = 0; i < _powerUpNum; i++)
        {
            _powerUpIndex.Add(i);
        }

        //�����_���Ƀ��X�g�̗v�f��I�ԁi�p���[�A�b�v�v�f��I�ԁj
        for (int i = 0; i < _button.Count; i++)
        {
            //�{�^���Ɋ���U��p���[�A�b�v�v�f���{�^���̐����������Ƃ��i�p���[�A�b�v�v�f��I�ׂ�Ƃ��j
            if (_button.Count - i <= _powerUpIndex.Count)
            {
                //��������
                int rand = Random.Range(0, _powerUpIndex.Count);
                //�{�^�����A�N�e�B�u��
                _button[i].SetActive(true);
                //�{�^���ɑ΂��Ĕԍ�������U��
                _button[i].GetComponent<PowerUp>().SetImage(_powerUpIndex[rand]);
                //�I�΂ꂽ�v�f���폜
                _powerUpIndex.RemoveAt(rand);
            }
        }
    }
}
