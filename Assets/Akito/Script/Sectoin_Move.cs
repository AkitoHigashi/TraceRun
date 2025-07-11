using UnityEngine;
/// <summary>
/// 床動かす
/// </summary>
public class Sectoin_Move : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Vector3.back * _moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// 判定基準の位置
    /// </summary>
    [SerializeField] private Vector3 DesPosition = new Vector3(0, 0, -100);

    void Update()
    {
        // Z座標で比較
        if (transform.position.z < DesPosition.z)
        {
            Destroy(gameObject);
        }
    }

}
