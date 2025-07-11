using UnityEngine;
/// <summary>
/// 判定をとる生成するための
/// </summary>
public class SectionTrigger : MonoBehaviour
{
    [SerializeField] GameObject _roadSectoin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))//トリガーのタグに反応する
        {
            Instantiate(_roadSectoin,new Vector3(0,0,300),Quaternion.identity);
            Debug.Log("生成されました");
        }
    }
}
