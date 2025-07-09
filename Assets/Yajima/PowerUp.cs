using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PowerUp : MonoBehaviour
{
    [Header("Sprites"), Tooltip("�{�^���̃X�v���C�g�̃��X�g")]
    [SerializeField]
    List<Sprite> _sprite;

    [Header("Player"), Tooltip("�v���C���[�̃I�u�W�F�N�g��ݒ�")]
    [SerializeField]
    GameObject _player;

    int _index;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// �{�^���̃C���[�W��ݒ肷��֐�
    /// </summary>
    /// <param name="index"> ���X�g�̃{�^���̃C���[�W���w�肷��ϐ�</param>
    public void SetImage(int index)
    {
        _index = index;
        gameObject.GetComponent<Image>().sprite = _sprite[_index];
    }

    /// <summary>
    /// �v���C���[�̃p���[�A�b�v������֐�
    /// �X�v���C�g�ƌĂяo���֐��̓C���f�b�N�X��Ή������邱��
    /// </summary>
    public void PlayerPowerUp()
    {
        switch (_index)
        {
            case 0:
                //�v���C���[�̃p���[�A�b�v����֐����v���C���[����Ăяo��
                _player.GetComponent<Player>().Hello();
                break;
            case 1:
                _player.GetComponent<Player>().GoodBye();
                break;
            case 2:
                _player.GetComponent<Player>().GoodMorning();
                break;
            case 3:
                _player.GetComponent<Player>().GoodEvening();
                break;
            default:
                break;
        }
    }
}
