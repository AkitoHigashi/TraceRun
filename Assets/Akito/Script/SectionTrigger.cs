using UnityEngine;
/// <summary>
/// ������Ƃ鐶�����邽�߂�
/// </summary>
public class SectionTrigger : MonoBehaviour
{
    [SerializeField] GameObject _roadSectoin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))//�g���K�[�̃^�O�ɔ�������
        {
            Instantiate(_roadSectoin,new Vector3(0,0,300),Quaternion.identity);
            Debug.Log("��������܂���");
        }
    }
}
