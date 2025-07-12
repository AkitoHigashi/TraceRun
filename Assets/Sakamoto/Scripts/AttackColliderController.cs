using UnityEngine;

public class AttackColliderController : MonoBehaviour
{
    BoxCollider _boxCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.enabled = true;
    }
    
    //�A�j���[�V�����C�x���g�ŌĂ�
    private void ColiderOn()
    {
        _boxCollider.enabled = true;
    }
    private void ColliderOff()
    {
        _boxCollider.enabled = false;
    }
}
