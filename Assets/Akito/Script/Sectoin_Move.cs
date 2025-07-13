using UnityEngine;
using UnityEngine.Rendering;
/// <summary>
/// è∞ìÆÇ©Ç∑
/// </summary>
public class Sectoin_Move : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private GameDirctor _gamedirctor;
    // Update is called once per frame
    private void Awake()
    {
        _gamedirctor = FindAnyObjectByType<GameDirctor>();
    }
    void FixedUpdate()
    {
        if (!_gamedirctor._isInBossBattle)
        {
            MoveGround();
        }
    }

    /// <summary>
    /// îªíËäÓèÄÇÃà íu
    /// </summary>
    [SerializeField] private Vector3 DesPosition = new Vector3(0, 0, -100);

    void Update()
    {
        // Zç¿ïWÇ≈î‰är
        if (transform.position.z < DesPosition.z)
        {
            Destroy(gameObject);
        }
    }

    void MoveGround()
    {
        transform.position += Vector3.back * _moveSpeed * Time.deltaTime;
    }

}
