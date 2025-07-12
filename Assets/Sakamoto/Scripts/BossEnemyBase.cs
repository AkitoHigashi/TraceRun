using UnityEngine;

public abstract class BossEnemyBase : MonoBehaviour
{
    protected float _speed;
    protected int _health;
    protected int _damage;
    protected float _attackCoolTime;

    public virtual void Setup(BossEnemyData data)
    {
        _speed = data.MoveSpeed;
        _health = data.Health;
        _damage = data.AttackDamage;
        _attackCoolTime = data.AttackCoolTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            //‰¼
            BulletDamage bullet = other.GetComponent<BulletDamage>();
            TakeDamage(bullet.damage);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "DeathFloor")
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStateTest player = collision.gameObject.GetComponent<PlayerStateTest>();
            if (player != null)
            {
                player.TakeDamage(_damage);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public enum BossState
    {
        Idle,
        Run,
        Die,
        Attack,
        Attack2,
    }
}
