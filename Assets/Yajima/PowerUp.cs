using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    [Header("Player"), Tooltip("プレイヤーのオブジェクトを設定")]
    [SerializeField]
    GameObject _player;

    [Header("ScriptableObjects"), Tooltip("パワーアップのデータを設定")]
    [SerializeField]
    PowerUpData _powerUpData;

    int _index;

    /// <summary>
    /// ボタンのイメージを設定する関数
    /// </summary>
    /// <param name="index"> リストのボタンのイメージを指定する変数</param>
    public void SetImage(int index)
    {
        if (_powerUpData == null)
        {
            Debug.LogWarning("ScriptableObjectが登録されていません");
        }
        else
        {
            _index = index;
            gameObject.GetComponent<Image>().sprite = _powerUpData._list[_index]._sprite;
        }
    }

    /// <summary>
    /// プレイヤーのパワーアップをする関数
    /// ボタンで呼び出す
    /// </summary>
    public void PlayerPowerUp()
    {
        _player.GetComponent<PlayerStatus>().PowerUp(_powerUpData._list[_index]._name, _powerUpData._list[_index]._powerUpValue);
    }
}
