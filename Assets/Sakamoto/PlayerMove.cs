using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _movexRange = 5;
    [SerializeField] private float _movezRange = 5;
    private InputBuffer _inputBuffer;
    private Rigidbody _rb;
    private Vector2 _currentInput; // 現在の入力を保持

    public void RegisterInputAction()
    {
        if (_inputBuffer != null)
        {
            _inputBuffer.MoveAction.performed += OnInputMove;
            _inputBuffer.MoveAction.canceled += OnInputMove;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _inputBuffer = FindAnyObjectByType<InputBuffer>();
        RegisterInputAction();
    }

    private void OnInputMove(InputAction.CallbackContext context)
    {
        // 入力値を保持（継続的に使用するため）
        _currentInput = context.ReadValue<Vector2>();
    }

    // FixedUpdateで移動処理と位置制限を適用
    void FixedUpdate()
    {
        Vector3 currentPos = transform.position;
        // 入力方向を3D空間に変換
        Vector3 inputDir = new Vector3(_currentInput.x, 0, _currentInput.y);
        // 各軸で移動可能かチェック
        Vector3 allowedInput = inputDir;
        // X軸の制限チェック
        if (currentPos.x >= _movexRange && inputDir.x > 0)
        {
            allowedInput.x = 0;
        }
        else if (currentPos.x <= -_movexRange && inputDir.x < 0)
        {
            allowedInput.x = 0;
        }
        // Z軸の制限チェック
        if (currentPos.z >= _movezRange && inputDir.z > 0)
        {
            allowedInput.z = 0;
        }
        else if (currentPos.z <= -_movezRange && inputDir.z < 0)
        {
            allowedInput.z = 0;
        }
        // 許可された入力で速度を設定
        Vector3 velocity = allowedInput.normalized * _moveSpeed;
        velocity.y = _rb.linearVelocity.y; // Y軸の速度は維持（重力など）

        _rb.linearVelocity = velocity;

        // 位置を範囲内に強制的に制限（安全のため）
        Vector3 clampedPos = currentPos;
        clampedPos.x = Mathf.Clamp(clampedPos.x, -_movexRange, _movexRange);
        clampedPos.z = Mathf.Clamp(clampedPos.z, -_movezRange, _movezRange);

        // 位置が変わった場合のみ更新
        if (clampedPos != currentPos)
        {
            transform.position = clampedPos;
        }
    }
}