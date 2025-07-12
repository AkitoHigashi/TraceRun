using UnityEngine;

public class Rock : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Lord")
        {
            this.gameObject.SetActive(false);
        }
    }
}
