using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected float _speed;
    protected int _health;
    protected int _damage;

    public virtual void Setup(EnemyData data)
    {
        _speed = data.MoveSpeed;
        _health = data.Health;
        _damage = data.AttackDamage;
    }
    public virtual void MoveToBase()
    {
        transform.Translate(Vector3.forward * -1 * _speed * Time.deltaTime);
    }
    public abstract void Attack();

    public enum enemyState
    {
        Idle,
        Run,
        Attack,
        Die,
    }
}
