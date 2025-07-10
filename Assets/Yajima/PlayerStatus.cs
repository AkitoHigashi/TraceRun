using UnityEngine;
using System.Collections.Generic;

public class PlayerStatus : MonoBehaviour
{
    //�v���C���[�ɃA�^�b�`
    [Header("PowerUpData(ScriptableObject)")]
    [SerializeField]
    PowerUpData _powerUpData;

    Dictionary<string, int> _parameter = new Dictionary<string, int>();

    private void Start()
    {
        MakeDictionary();
    }

    /// <summary>
    /// �p�����[�^�̎������쐬����֐�
    /// </summary>
    void MakeDictionary()
    {
        if (_powerUpData == null)
        {
            Debug.LogWarning("ScriptableObject���o�^����Ă��܂���");
        }
        else
        {
            for (int i = 0; i < _powerUpData._list.Count; i++)//_parameter��string���w�肷��Ə����l���l���ł���
            {
                _parameter[_powerUpData._list[i]._name] = _powerUpData._list[i]._defaultValue;
            }
        }
    }

    /// <summary>
    /// �p���[�A�b�v����֐�
    /// </summary>
    /// <param name="parameterName"> �p���[�A�b�v�̖��O</param>
    /// <param name="powerUp"> �㏸�l</param>
    public void PowerUp(string parameterName, int powerUp)
    {
        _parameter[parameterName] += powerUp;
    }
}
