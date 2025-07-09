using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyFactoryManager : MonoBehaviour
{
    [SerializeField] private EnemyDatabase database;

    private Dictionary<string, EnemyData> enemyDict = new Dictionary<string, EnemyData>();
    private void Awake()
    {
        foreach (var data in database.enemyList)
        {
            if (!enemyDict.ContainsKey(data.TypeName))
            {
                enemyDict[data.TypeName] = data;
            }
        }

    }

    public EnemyBase CreateEnemy(string type, Vector3 position)
    {
        if (enemyDict.TryGetValue(type, out var data))
        {
            GameObject enemyGO = Instantiate(data.Prefab, position, data.Prefab.transform.rotation);
            EnemyBase enemy = enemyGO.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                enemy.Setup(data);
            }
            return enemy;
        }

        Debug.LogError($"“Gƒ^ƒCƒv '{type}' ‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ");
        return null;
    }
    public List<string> GetAvailableTypes()
    {
        return new List<string>(enemyDict.Keys);
    }
}
