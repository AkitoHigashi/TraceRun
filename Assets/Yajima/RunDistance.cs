using UnityEngine;

public class RunDistance : MonoBehaviour
{
    [Header("Player"), SerializeField]
    Transform _player;

    Vector3 _startPos;
    Vector3 _nowPos;
    float _distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPos = _player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _nowPos = _player.transform.position - _startPos;
        _distance = _nowPos.magnitude;
        Debug.Log(_distance);
    }
}
