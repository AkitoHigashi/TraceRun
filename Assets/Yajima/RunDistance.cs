using UnityEngine;

public class RunDistance : MonoBehaviour
{
    [Header("Meter/deltaTime"), Tooltip("�P�t���[���Ői�ދ���")]
    [SerializeField]
    float _meterPerSeconds;
    float _distance;
    float _elapsedTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _distance = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;//�o�ߎ��Ԃ��v��
        _distance = _meterPerSeconds * _elapsedTime;//�P�t���[���Ői�ދ����~�o�ߎ��ԁ��i�񂾋���
        Debug.Log(_distance);
    }
}
