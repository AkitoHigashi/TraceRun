using UnityEngine;

public class PlayerStateTest : MonoBehaviour
{
    [SerializeField] private int _health=100;
    [SerializeField] private SceneChange _SC;
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
        _SC.ChangeScene("result");
        Debug.Log("‚µ‚ñ‚¾");
    }
}
