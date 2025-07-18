using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    [Header("Player"), Tooltip("プレイヤーのオブジェクトを設定")]
    [SerializeField]
    GameObject _player;

    [SerializeField, Header("弾のプレハブ")]
    GameObject _ammoPre;

    [SerializeField, Header("インクの管理オブジェクト")]
    GameObject _canvas;

    [Header("ScriptableObjects"), Tooltip("パワーアップのデータを設定")]
    [SerializeField]
    PowerUpData _powerUpData;


    int _index;

    /// <summary>
    /// ボタンのイメージを設定する関数
    /// </summary>
    /// <param name="index"> リストのボタンのイメージを指定する変数</param>
    public void SetText(int index)
    {
        if (_powerUpData == null)
        {
            Debug.LogWarning("ScriptableObjectが登録されていません");
        }
        else
        {
            _index = index;
            gameObject.GetComponent<Text>().text = _powerUpData._list[_index]._Text;
        }
    }

    /// <summary>
    /// プレイヤーのパワーアップをする関数
    /// ボタンで呼び出す
    /// </summary>
    public void PlayerPowerUp()
    {
        _ammoPre.GetComponent<Ammo>()
            .AmmoUpGrade(_powerUpData._list[_index]._ammoSpeedUP, _powerUpData._list[_index]._ammoMaxPosUP);
        _player.GetComponent<PlayerStateTest>()
            .recovery(_powerUpData._list[_index]._hpRecovery);
        _player.GetComponent<PlayerMove>()
            .MoveUp(_powerUpData._list[_index]._moveSpeedUP);
        _canvas.GetComponent<DrawLine>().UpGrade(_powerUpData._list[_index]._maxInkUP);
    }
}
