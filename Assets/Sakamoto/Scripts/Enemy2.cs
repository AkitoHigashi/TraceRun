using UnityEngine;

public class Enemy2 : EnemyBase
{
    [SerializeField] private float _enemySpeed = 5f;
    private void Awake()
    {
        _speed = _enemySpeed;
    }
    private void Update()
    {
        MoveToBase();
    }
    public override void Attack()
    {

    }
}
