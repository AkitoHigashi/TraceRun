using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    int _enemyCount = 0;

    /// <summary>
    /// “G‚ğ“|‚µ”‚ğƒJƒEƒ“ƒg‚·‚éŠÖ”
    /// “G‚ª€‚Ê‚Æ‚«‚ÉŒÄ‚Ño‚¹‚Î‚¢‚¢‚Í‚¸
    /// </summary>
    public void EnemyDead()
    {
        _enemyCount++;
        Debug.Log(_enemyCount+"‘Ì‚Ì“G‚ğ“|‚µ‚½");
    }
}
