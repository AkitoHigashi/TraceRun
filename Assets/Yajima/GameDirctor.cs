using UnityEngine;
using UnityEngine.UI;

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
        //�o�ߎ��Ԃ��v��
        _elapsedTime += Time.deltaTime;

        //�P�t���[���Ői�ދ����~�o�ߎ��ԁ��i�񂾋���
        _distance = _meterPerFrame * _elapsedTime;

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
            _scoreText.text = _score.ToString("00000");
        }
        else
        {
            Debug.LogWarning("�X�R�A��\�����邽�߂̃e�L�X�g������܂���");
        }

        //�����e�L�X�g
        if (_distanceText != null)
        {
            _distanceText.text = _distance.ToString("0000.0") + "m";
        }
        else
        {
            Debug.LogWarning("������\�����邽�߂̃e�L�X�g������܂���");
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

    /// <summary>
    /// �G��|�������J�E���g����֐�
    /// �G�����ʂƂ��ɌĂяo���΂����͂�
    /// </summary>
    public void EnemyDead()
    {
        _enemyCount++;
    }

    /// <summary>
    /// �X�R�A�����Z����֐�
    /// </summary>
    /// <param name="score"> �|�����G�ɉ������X�R�A</param>
    public void AddScore(int score)
    {
        _score += score;
    }
}
