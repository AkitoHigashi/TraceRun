using UnityEngine;

public class LongAttack : MonoBehaviour
{
    [SerializeField] GameObject _stone;
    [SerializeField] float _stoneheight=10f;
    GameObject player;
    private void Start()
    {
        player = GameObject.FindWithTag ("Player" );  
    }
    //アニメーションイベントで呼ぶ
    public void FallStone()
    {
        if (player != null)
        {
            Vector3 spawnPos = player.transform.position + Vector3.up * _stoneheight;
            Instantiate(_stone, spawnPos, Quaternion.identity);
        }
    }
}
