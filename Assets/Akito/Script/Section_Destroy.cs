using UnityEngine;
/// <summary>
/// ���f�X�g���C
/// </summary>
public class Section_Destroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lord"))//Lord�^�O�t����
        {
            Destroy(other.gameObject);
            Debug.Log("�폜���܂���");
        }
    }
}
