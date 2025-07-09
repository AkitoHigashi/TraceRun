using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField] DrawPanel _drawPanel;
    [SerializeField]Ammo _ammo;
    void Update()
    {
        Vector2 transform = _drawPanel.ManualUpdate();
        _ammo.DrawLine(transform);
    }
}
