using UnityEngine;
/// <summary>
/// �I�u�W�F�N�g���s�����͂���N���X
/// </summary>
public class Size : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       float length =  GetStageLength(gameObject);
        Debug.Log("���s����"+length);
    }

    private float GetStageLength(GameObject obj)
    {
        var meshRender = obj.GetComponent<MeshRenderer>();//���b�V�������_���[���擾�B
        return meshRender.bounds.size.z;//�����_���[�̉��s�����擾���ĕԂ�

    }
}
