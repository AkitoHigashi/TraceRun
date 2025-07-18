using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonScaleAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _scaleAmount = 1.2f;//拡大率
    [SerializeField] private float _duration = 0.3f;//拡大までの時間

    private Vector3 _originalScale;

    private void OnEnable()
    {
        if (_originalScale == Vector3.zero)//一度だけ
        {
            _originalScale = transform.localScale;//オブジェクトのスケールを取得

        }
        transform.localScale = _originalScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(_originalScale * _scaleAmount, _duration)
            .SetUpdate(true)
            .SetEase(Ease.OutBack);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(_originalScale, _duration)
            .SetUpdate(true)
            .SetEase(Ease.OutBack);
    }
}
