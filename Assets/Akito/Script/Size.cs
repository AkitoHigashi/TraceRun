using UnityEngine;
/// <summary>
/// オブジェクト奥行きをはかるクラス
/// </summary>
public class Size : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       float length =  GetStageLength(gameObject);
        Debug.Log("奥行きは"+length);
    }

    private float GetStageLength(GameObject obj)
    {
        var meshRender = obj.GetComponent<MeshRenderer>();//メッシュレンダラーを取得。
        return meshRender.bounds.size.z;//レンダラーの奥行きを取得して返す

    }
}
