using UnityEngine;
using System.Collections.Generic;

public class PlayerStatus : MonoBehaviour
{
    //プレイヤーにアタッチ
    [Header("PowerUpData(ScriptableObject)")]
    [SerializeField]
    PowerUpData _powerUpData;

    Dictionary<string, int> _parameter = new Dictionary<string, int>();

    private void Start()
    {
        MakeDictionary();
    }

    /// <summary>
    /// パラメータの辞書を作成する関数
    /// </summary>
    void MakeDictionary()
    {
        if (_powerUpData == null)
        {
            Debug.LogWarning("ScriptableObjectが登録されていません");
        }
        else
        {
            for (int i = 0; i < _powerUpData._list.Count; i++)//_parameterにstringを指定すると初期値を獲得できる
            {
                _parameter[_powerUpData._list[i]._name] = _powerUpData._list[i]._defaultValue;
            }
        }
    }

    /// <summary>
    /// パワーアップする関数
    /// </summary>
    /// <param name="parameterName"> パワーアップの名前</param>
    /// <param name="powerUp"> 上昇値</param>
    public void PowerUp(string parameterName, int powerUp)
    {
        _parameter[parameterName] += powerUp;
    }
}
