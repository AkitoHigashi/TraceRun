using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    int _enemyCount = 0;

    /// <summary>
    /// �G��|�������J�E���g����֐�
    /// �G�����ʂƂ��ɌĂяo���΂����͂�
    /// </summary>
    public void EnemyDead()
    {
        _enemyCount++;
        Debug.Log(_enemyCount+"�̂̓G��|����");
    }
}
