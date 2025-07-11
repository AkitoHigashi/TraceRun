using UnityEngine;
/// <summary>
/// è∞ìÆÇ©Ç∑
/// </summary>
public class Sectoin_Move : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += Vector3.back * _moveSpeed * Time.deltaTime;
    }

}
