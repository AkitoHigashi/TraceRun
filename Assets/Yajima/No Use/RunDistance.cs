using UnityEngine;

public class RunDistance : MonoBehaviour
{
    [Header("Meter/deltaTime"), Tooltip("１フレームで進む距離")]
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
        _elapsedTime += Time.deltaTime;//経過時間を計測
        _distance = _meterPerSeconds * _elapsedTime;//１フレームで進む距離×経過時間＝進んだ距離
        Debug.Log(_distance);
    }
}
