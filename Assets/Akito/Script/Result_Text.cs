using UnityEngine;
using UnityEngine.UI;

public class Result_Text : MonoBehaviour
{
    [SerializeField] Text _runDistanceText;
    [SerializeField] Text _scoreCountsText;
    [SerializeField] Text _enemyKillCountsText;


    float _scoreCounts;
    float _runDistance;
    int _enemyKillCounts;

    private void Awake()
    {
        _runDistance = GameDirctor._distance;
        _scoreCounts = GameDirctor._nowScore;
        _enemyKillCounts = GameDirctor._enemyCount;
    }
    private void Start()
    {
        _runDistanceText.text = "ëñÇ¡ÇΩãóó£  " + _runDistance.ToString("0000.0") + "m";
        _scoreCountsText.text = "ÉXÉRÉA  " + _scoreCounts.ToString("00000");
        _enemyKillCountsText.text = "ì|ÇµÇΩìGÇÃêî      " + _enemyKillCounts + "ëÃ";


    }
}
