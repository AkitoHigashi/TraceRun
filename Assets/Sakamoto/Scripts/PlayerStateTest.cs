using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerStateTest : MonoBehaviour
{
    [SerializeField] private float _maxhealth = 100f;
    [SerializeField, Header("FillAmountが設定されているImageをアタッチしてください")] private Image _hpImage;
    [SerializeField] private SceneChange _SC;
    private float _cureentHealth;
    private void Start()
    {
        _cureentHealth = _maxhealth;
    }
    public void TakeDamage(int damage)
    {
        _cureentHealth -= damage;
        float normalized = _cureentHealth / _maxhealth;
        _hpImage.DOFillAmount(normalized, 0.3f).SetEase(Ease.OutQuad);

        if (_cureentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        _SC.ChangeScene("result");
        Debug.Log("しんだ");
    }
    public void recovery(float recohealth)
    {
        _cureentHealth += recohealth;
        float normalized = _cureentHealth / _maxhealth;
        _hpImage.DOFillAmount(normalized, 0.3f).SetEase(Ease.OutQuad);
    }
}
