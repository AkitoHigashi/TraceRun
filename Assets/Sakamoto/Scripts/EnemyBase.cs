using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected float _speed = 1.0f;

    public virtual void MoveToBase()
    {
        transform.Translate(Vector3.forward * -1 * _speed * Time.deltaTime);
    }
    public abstract void Attack();
}
