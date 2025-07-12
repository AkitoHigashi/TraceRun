using UnityEngine;
/// <summary>
/// 床デストロイ
/// </summary>
public class Section_Destroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lord"))//Lordタグ付けて
        {
            Destroy(other.gameObject);
            Debug.Log("削除しました");
        }
    }
}
