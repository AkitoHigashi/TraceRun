using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PowerUp : MonoBehaviour
{
    [Header("Sprites"), Tooltip("ボタンのスプライトのリスト")]
    [SerializeField]
    List<Sprite> _sprite;

    [Header("Player"), Tooltip("プレイヤーのオブジェクトを設定")]
    [SerializeField]
    GameObject _player;

    int _index;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// ボタンのイメージを設定する関数
    /// </summary>
    /// <param name="index"> リストのボタンのイメージを指定する変数</param>
    public void SetImage(int index)
    {
        _index = index;
        gameObject.GetComponent<Image>().sprite = _sprite[_index];
    }

    /// <summary>
    /// プレイヤーのパワーアップをする関数
    /// スプライトと呼び出す関数はインデックスを対応させること
    /// </summary>
    public void PlayerPowerUp()
    {
        switch (_index)
        {
            case 0:
                //プレイヤーのパワーアップする関数をプレイヤーから呼び出す
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
