using Unity.VisualScripting;
using UnityEngine;

public class Boss_instance : MonoBehaviour
{
    private GameObject _Bosspos;
    [SerializeField] private BossEnemyData _bossDate;
    [SerializeField] private GameDirctor _gameDirctor;
    bool SpawnBoss = false;
    private void Awake()
    {
        _Bosspos = GameObject.FindGameObjectWithTag("SpawnPos");
    }
    private void Update()
    {
        if (_gameDirctor._isInBossBattle && !SpawnBoss)
        {
            GameObject boss = Instantiate(_bossDate.Prefab, _Bosspos.transform.position, _bossDate.Prefab.transform.rotation);
            var bossComponent = boss.GetComponent<BossEnemyBase>();
            if (bossComponent != null)
            {
                bossComponent.Setup(_bossDate); // BossEnemyBase に Setup(BossEnemyData data) メソッドを用意
                SpawnBoss = true;
            }
            else
            {
                Debug.LogWarning("BossEnemyData または Prefab が設定されていません");
            }

        }
        else
        {
            //Debug.Log("生成済みです");
        }

    }
}
