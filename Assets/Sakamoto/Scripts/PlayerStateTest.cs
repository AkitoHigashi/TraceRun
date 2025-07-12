using UnityEngine;

public class PlayerStateTest : MonoBehaviour
{
    [SerializeField] private int _health=100;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if(_health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("‚µ‚ñ‚¾");
    }
}
