using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Lord")
        {
            this.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStateTest player = collision.gameObject.GetComponent<PlayerStateTest>();
            if (player != null)
            {
                Debug.Log(_damage);
                player.TakeDamage(_damage);
            }

            gameObject.SetActive(false);
        }
    }
}
