using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField]Color lineColor = Color.red; // 線の色を設定
    List<Vector2> _positions = new List<Vector2>();
    private LineRenderer _lineRenderer;
    public float yPosition = 1.0f; // Y軸の高さを調整可能
    public LayerMask raycastLayerMask = -1; // レイキャスト対象のレイヤー
    public int interpolationSteps = 5; // 補間のステップ数
    
    private void Start()
    {
        // LineRendererコンポーネントを取得または追加
        _lineRenderer = GetComponent<LineRenderer>();
        if (_lineRenderer == null)
        {
            _lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        
        // LineRendererの設定
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startColor = lineColor; // 線の色を赤に設定
        _lineRenderer.endColor = lineColor; // 線の色を赤に設定
        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;
        _lineRenderer.useWorldSpace = true;
    }
    
    public void DrawLine(Vector2 position)
    {
        position = position / 10f;
        
        // 線形補間で点を追加
        if (_positions.Count > 0)
        {
            Vector2 lastPosition = _positions[_positions.Count - 1];
            InterpolatePoints(lastPosition, position);
        }
        else
        {
            _positions.Add(position);
        }
        
        // Vector2をVector3に変換（x,zに使用、yは設定値）
        Vector3 worldPosition = new Vector3(position.x, yPosition, position.y);
        
        // レイキャストを実行
        PerformRaycast(worldPosition);
        
        // ラインレンダラーで線を描画
        UpdateLineRenderer();
    }
    // 補間された点を追加するメソッド
    private void InterpolatePoints(Vector2 from, Vector2 to)
    {
        float distance = Vector2.Distance(from, to);
        int steps = Mathf.Max(1, Mathf.RoundToInt(distance * interpolationSteps));
        
        for (int i = 1; i <= steps; i++)
        {
            float t = i / (float)steps;
            Vector2 interpolatedPosition = Vector2.Lerp(from, to, t);
            _positions.Add(interpolatedPosition);
        }
    }
    // レイキャストを実行するメソッド
    private void PerformRaycast(Vector3 position)
    {
        // 上から下へのレイキャストを実行
        Vector3 rayOrigin = position + Vector3.up * 50f;
        Vector3 rayDirection = Vector3.down;
        
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, 20f, raycastLayerMask))
        {
            Debug.Log($"Hit: {hit.collider.name} at {hit.point}");
            // 当たった位置を使用する場合
            // position = hit.point;
        }
    }
    // ラインレンダラーを更新するメソッド
    private void UpdateLineRenderer()
    {
        if (_positions.Count > 1)
        {
            _lineRenderer.positionCount = _positions.Count;
            
            for (int i = 0; i < _positions.Count; i++)
            {
                Vector3 worldPos = new Vector3(_positions[i].x, yPosition, _positions[i].y);
                _lineRenderer.SetPosition(i, worldPos);
            }
        }
    }
}
