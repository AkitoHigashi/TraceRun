using System;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField] DrawPanel _drawPanel;
    [SerializeField] Ammo _ammoPrefab; // DrawPanelのプレハブ
    Ammo _ammo;
    void Start()
    {
        _drawPanel.ResetAction = Reset; // DrawPanelのResetActionにResetメソッドを登録
        _ammo = Instantiate(_ammoPrefab);
        _drawPanel.DragAction = DragUpdate; // DrawPanelのDragActionにDragUpdateメソッドを登録
    }
    void DragUpdate(Vector2 position)
    {
        _ammo.DrawLine(position);
    }
    void Reset()
    {
        _ammo.Shoot();
        _ammo = Instantiate(_ammoPrefab);
    }
}
