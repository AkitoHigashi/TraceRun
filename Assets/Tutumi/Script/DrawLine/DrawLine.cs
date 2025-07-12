using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class DrawLine : MonoBehaviour
{
    [SerializeField] DrawPanel _drawPanel;
    [SerializeField] Ammo _ammoPrefab; // DrawPanelのプレハブ
    [SerializeField] GameObject _playerObj; // DrawPanelの親オブジェクト
    [SerializeField] float _aomountReduced = 0.1f; // DrawPanelのインク量を減らす値
    [SerializeField,Header("インクの最大値を設定してください")]float _maxInk = 100f; // インクの最大値
    [SerializeField, Header("FillAmountが設定されているImageをアタッチしてください")] Image _guageImage;//ゲージのImage
    Ammo _ammo;
    float _ink = 0f; // 現在のインク量
    void Start()
    {
        _ink = _maxInk; // 初期インク量を最大値に設定
        _drawPanel.ResetAction = Reset; // DrawPanelのResetActionにResetメソッドを登録
        _ammo = Instantiate(_ammoPrefab);
        _ammo.RootPosSet(_playerObj); // Ammoの親オブジェクトを設定
        _drawPanel.DragAction = DragUpdate; // DrawPanelのDragActionにDragUpdateメソッドを登録
    }
    void DragUpdate(Vector2 position)
    {
        if (_ink <= 0) return; // インクがない場合は何もしない
        _ink -= _aomountReduced; // インクを減らす
        Debug.Log($"インクの量: {_ink}");
        _ammo.DrawLine(position);
        if (!_guageImage) return;//ゲージのImageが設定されていない場合は何もしない
        _guageImage.fillAmount = _ink / _maxInk; // ゲージのFillAmountを更新
    }
    /// <summary>
    /// インクの量を更新するメソッド
    /// </summary>
    /// <param name="ink">追加するインクの量を指定してください</param>
    public void InkHealthUpdate(float ink)
    {
        _ink += ink; // インクの量を更新
    }
    void Reset()
    {
        _ammo.Shoot().Forget();
        _ammo = Instantiate(_ammoPrefab);
        _ammo.RootPosSet(_playerObj); // Ammoの親オブジェクトを設定
    }
}
