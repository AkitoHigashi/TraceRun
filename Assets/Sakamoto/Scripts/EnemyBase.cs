using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected float _speed;
    protected int _health;
    protected int _damage;
    protected int _healInk;

    public virtual void Setup(EnemyData data)
    {
        _speed = data.MoveSpeed;
        _health = data.Health;
        _damage = data.AttackDamage;
        _healInk = data.HealInk;
    }
    public virtual void MoveToBase()
    {
        transform.Translate(Vector3.forward * -1 * _speed * Time.deltaTime);
    }
    public abstract void Attack();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            //��
            BulletDamage bullet = other.GetComponent<BulletDamage>();
            TakeDamage(bullet.damage);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "DeathFloor")
        {
            this.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy took damage: " + damage);
        _health -=damage;
        if (_health <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        GameObject.FindAnyObjectByType<DrawLine>()?.InkHealthUpdate(_healInk); // インクを回復
        this.gameObject.SetActive(false);
    }

    public enum enemyState
    {
        Idle,
        Run,
        Attack,
        Die,
    }
}
