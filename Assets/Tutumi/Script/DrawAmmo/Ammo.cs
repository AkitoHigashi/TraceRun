using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cysharp.Threading.Tasks;
using System.Threading;

public class Ammo : MonoBehaviour
{
    [SerializeField,Header("線の色を設定してください")] private Color _lineColor = Color.red; // 線の色を設定
    [SerializeField, Header("敵に当たった時のダメージを設定してください")] private int _damage = 10;
    private GameObject _rootObj;
    [SerializeField, Header("球の移動速度を設定してください")] private float _ammoSpeed = 0.1f; // 弾の移動速度
    [SerializeField,Header("球が消える位置を設定してください")] private float _ammoMaxPos = 100f;//球の最大位置
    [SerializeField, Header("球の大きさを設定してください割り算なので数値を大きくするほど球が小さくなります")] private float _ammoSize = 50f; // レイキャスト対象のレイヤー
    private List<Vector2> _positions = new List<Vector2>();
    private List<bool> _isHit = new List<bool>(); // 各点がヒットしたかどうかのリスト
    private LineRenderer _lineRenderer;
    private LayerMask _raycastLayerMask = -1; // レイキャスト対象のレイヤー
    private int _interpolationSteps = 5; // 補間のステップ数
    public float AmmoSize { get { return _ammoSize; } }// Ammoの大きさを取得するプロパティ
    bool _isShooting = false; // 発射中かどうかのフラグ
    Vector2 _beforePosition;

    float _height;

    private void Start()
    {
        _beforePosition = Vector2.zero; // 初期化
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
        position = position / _ammoSize;
        Vector2 rootPosition2D = new Vector2(_rootObj.transform.position.x, _rootObj.transform.position.z);
        _height = _rootObj.transform.position.y;
        position += rootPosition2D;
        if (_positions.Count > 0)
        {
            Vector2 lastPosition = _positions[_positions.Count - 1];
            InterpolatePoints(lastPosition, position);
        }
        else
        {
            _positions.Add(position);
            _isHit.Add(false); // ヒット状態を初期化
        }
        UpdateLineRenderer();
    }
    private void UpdateLinePosition()
    {
        Vector2 rootPos = new Vector2(_rootObj.transform.position.x, _rootObj.transform.position.z);
        for (int i = 0; i < _positions.Count; i++)
        {
            if(_beforePosition != Vector2.zero)_positions[i] -= _beforePosition; // 前の位置を引く
            _positions[i] += rootPos; // ルートオブジェクトの位置を加算
        }
        _beforePosition = rootPos; // 現在の位置を保存
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
            _isHit.Add(false); // ヒット状態を初期化
        }
    }
    // レイキャストを実行するメソッド
    private async UniTask PerformRaycast(Vector3 rayStart, Vector3 rayEnd)
    {
        // 方向と距離を計算
        Vector3 rayDirection = (rayEnd - rayStart).normalized;
        float rayDistance = Vector3.Distance(rayStart, rayEnd);

        if (Physics.Raycast(rayStart, rayDirection, out RaycastHit hit, rayDistance, _raycastLayerMask))
        {
            Debug.Log($"Hit: {hit.collider.name} at {hit.point}");
            if (hit.collider.gameObject.TryGetComponent<EnemyBase>(out EnemyBase enemy))
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                enemy.TakeDamage(_damage); // ダメージを与える
                return;
            }
        }
    }
    void Update()
    {
        if (_isShooting) return; // 発射中でない場合は何もしない
        UpdateLinePosition();
        UpdateLineRenderer();
    }
    // ラインレンダラーを更新するメソッド
    private void UpdateLineRenderer()
    {

        if (_positions.Count > 1)
        {
            _lineRenderer.positionCount = _positions.Count;
            for (int i = 0; i < _positions.Count; i++)
            {
                if (_isHit[i]) continue;
                Vector3 worldPos = new Vector3(_positions[i].x, _height, _positions[i].y);
                _lineRenderer.SetPosition(i, worldPos);
            }
        }
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
                PerformRaycast(new Vector3(_positions[i].x,_height,_positions[i].y), new Vector3(_positions[i + 1].x,_height,_positions[i + 1].y)).Forget(); // レイキャストを実行
            }
            await UniTask.Delay(10, cancellationToken: cancellationToken); // 100ミリ秒待機
            UpdateLineRenderer(); // ラインレンダラーを更新
        } while (isAmmoInRange);
        Debug.Log("Shoot End");
        Destroy(gameObject); // Ammoオブジェクトを削除
    }
}
