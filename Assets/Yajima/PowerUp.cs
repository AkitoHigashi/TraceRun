using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    [Header("Player"), Tooltip("�v���C���[�̃I�u�W�F�N�g��ݒ�")]
    [SerializeField]
    GameObject _player;

    [Header("ScriptableObjects"), Tooltip("�p���[�A�b�v�̃f�[�^��ݒ�")]
    [SerializeField]
    PowerUpData _powerUpData;

    int _index;

    /// <summary>
    /// �{�^���̃C���[�W��ݒ肷��֐�
    /// </summary>
    /// <param name="index"> ���X�g�̃{�^���̃C���[�W���w�肷��ϐ�</param>
    public void SetImage(int index)
    {
        if (_powerUpData == null)
        {
            Debug.LogWarning("ScriptableObject���o�^����Ă��܂���");
        }
        else
        {
            _index = index;
            gameObject.GetComponent<Image>().sprite = _powerUpData._list[_index]._sprite;
        }
    }

    /// <summary>
    /// �v���C���[�̃p���[�A�b�v������֐�
    /// �{�^���ŌĂяo��
    /// </summary>
    public void PlayerPowerUp()
    {
        _player.GetComponent<PlayerStatus>().PowerUp(_powerUpData._list[_index]._name, _powerUpData._list[_index]._powerUpValue);
    }
}
