using UnityEngine;

public class GameDirctor : MonoBehaviour
{
    [Header("Meter/deltaTime"), Tooltip("�P�t���[���Ői�ދ���")]
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
        _elapsedTime += Time.deltaTime;//�o�ߎ��Ԃ��v��
        _distance = _meterPerFrame * _elapsedTime;//�P�t���[���Ői�ދ����~�o�ߎ��ԁ��i�񂾋���
        Debug.Log(_distance);
    }

    /// <summary>
    /// �G��|�������J�E���g����֐�
    /// �G�����ʂƂ��ɌĂяo���΂����͂�
    /// </summary>
    public void EnemyDead()
    {
        _enemyCount++;
        Debug.Log(_enemyCount + "�̂̓G��|����");
    }
}
