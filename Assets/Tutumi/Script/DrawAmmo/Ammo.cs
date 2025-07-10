using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cysharp.Threading.Tasks;
using System.Threading; // UniTaskを使用するための名前空間

public class Ammo : MonoBehaviour
{
    [SerializeField,Header("線の色を設定してください")] private Color _lineColor = Color.red; // 線の色を設定
    private GameObject _rootObj;
    [SerializeField,Header("球の移動速度を設定してください")]private float _ammoSpeed = 0.1f; // 弾の移動速度
    [SerializeField,Header("球が消える位置を設定してください")] private float _ammoMaxPos = 100f;//球の最大位置
    [SerializeField, Header("球の大きさを設定してください割り算なので数値を大きくするほど球が小さくなります")] private float _ammoSize = 50f; // レイキャスト対象のレイヤー
    private List<Vector2> _positions = new List<Vector2>();
    private LineRenderer _lineRenderer;
    private LayerMask _raycastLayerMask = -1; // レイキャスト対象のレイヤー
    private int _interpolationSteps = 5; // 補間のステップ数
    public float AmmoSize { get { return _ammoSize; } }// Ammoの大きさを取得するプロパティ
    bool _isShooting = false; // 発射中かどうかのフラグ
    Vector3 _beforePosition;

    private void Start()
    {
        // LineRendererコンポーネントを取得または追加
        _lineRenderer = GetComponent<LineRenderer>();
        if (_lineRenderer == null)
        {
            _lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startColor = _lineColor; // 線の色を赤に設定
        _lineRenderer.endColor = _lineColor; // 線の色を赤に設定
        _lineRenderer.startWidth = 0.1f;
        _lineRenderer.endWidth = 0.1f;
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.positionCount = 0; // 初期状態では点を持たない
    }
    public void RootPosSet(GameObject rootObj) 
    {
        _rootObj = rootObj;
    }

    public void DrawLine(Vector2 position)
    {
        position = position / _ammoSize; // スケールを適用

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
        Vector3 worldPosition = new Vector3(_rootObj.transform.position.x + _rootObj.transform.position.x, _rootPosition.y, _rootPosition.z = position.y);
        // ラインレンダラーで線を描画
        UpdateLineRenderer();
    }
    // 補間された点を追加するメソッド
    private void InterpolatePoints(Vector2 from, Vector2 to)
    {
        float distance = Vector2.Distance(from, to);
        int steps = Mathf.Max(1, Mathf.RoundToInt(distance * _interpolationSteps));

        for (int i = 1; i <= steps; i++)
        {
            float t = i / (float)steps;
            Vector2 interpolatedPosition = Vector2.Lerp(from, to, t);
            _positions.Add(interpolatedPosition);
        }
    }
    // レイキャストを実行するメソッド
    private void PerformRaycast(Vector3 rayStart, Vector3 rayEnd)
    {
        // 方向と距離を計算
        Vector3 rayDirection = (rayEnd - rayStart).normalized;
        float rayDistance = Vector3.Distance(rayStart, rayEnd);

        if (Physics.Raycast(rayStart, rayDirection, out RaycastHit hit, rayDistance, _raycastLayerMask))
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
                Vector3 worldPos = new Vector3(_positions[i].x, _rootPosition.y, _positions[i].y);
                _lineRenderer.SetPosition(i, worldPos);
            }
        }
    }
    void Update()
    {
        if (_isShooting) return;

    }
    public async UniTask Shoot()
    {
        _isShooting = true; // 発射中フラグを設定
        bool isAmmoInRange = true;
        CancellationToken cancellationToken = this.GetCancellationTokenOnDestroy();
        Debug.Log("Shoot Start");
        do
        {
            isAmmoInRange = false; // 初期状態をfalseに設定
            for (int i = 0; i < _positions.Count; i++)
            {
                _positions[i] += Vector2.up * _ammoSpeed; // 上方向に少しずつ移動
                if (_positions[i].y < _ammoMaxPos)
                {
                    isAmmoInRange = true; // 1つでも範囲内ならtrueに設定
                }
            }
            for (int i = 0; i < _positions.Count - 1; i++)
            {
                PerformRaycast(_positions[i], _positions[i + 1]); // レイキャストを実行
            }
            await UniTask.Delay(10, cancellationToken: cancellationToken); // 100ミリ秒待機
            UpdateLineRenderer(); // ラインレンダラーを更新
        } while (isAmmoInRange);
        Debug.Log("Shoot End");
        Destroy(gameObject); // Ammoオブジェクトを削除
    }
}
