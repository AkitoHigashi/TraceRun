using System.Collections.Generic;
using UnityEngine;



public class Ground_Generator : MonoBehaviour
{
    [SerializeField] private GameObject[] _stagePrefabs;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private int _count = 5;
    [SerializeField] private float _spawnZ;
    private List<GameObject> _activeStages = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; _count > i; i++)
        {
            SpawnNextStage();
        }
    }
    private void Update()
    {
        MoveStages();

        //Debug.Log("_activeStages count: " + _activeStages.Count);
    }

    /// <summary>
    /// 全ての地面オブジェクトを後ろに移動させる
    /// </summary>
    private void MoveStages()
    {
        for (int i = 0; i < _activeStages.Count; i++)
        {
            if (_activeStages[i] != null)
            {
                _activeStages[i].transform.Translate(Vector3.back * _moveSpeed * Time.deltaTime);
            }
        }
    }
    void SpawnNextStage()
    {
        // ランダムにプレハブを選ぶ
        GameObject prefab = _stagePrefabs[Random.Range(0, _stagePrefabs.Length)];


        // インスタンス生成
        GameObject stage = Instantiate(prefab, Vector3.forward * _spawnZ, Quaternion.identity);
        float length = GetStageLength(stage);
        Debug.Log(length);
        length = length * 0.98f;//0.98fでプレハブを継ぎ目をなくすようにしている。

        _activeStages.Add(stage);
        _spawnZ += length; // 次のZ座標を更新
    }
    private float GetStageLength(GameObject obj)
    {
        var meshRender = obj.GetComponent<MeshRenderer>();//メッシュレンダラーを取得。
        return meshRender.bounds.size.z;//レンダラーの奥行きを取得して返す
    }
    

}
