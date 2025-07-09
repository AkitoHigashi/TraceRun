using System;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrawPanel : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private RawImage _rawImage;
    Texture2D _texture;
    Vector2 _trasform;
    public int lineWidth = 5; // 線の太さを調整するパラメータ
    private Vector2 _previousPosition;
    private bool _isDragging = false;

    private void Start()
    {
        _texture = new Texture2D((int)((RectTransform)transform).rect.width, (int)((RectTransform)transform).rect.height, TextureFormat.RGBA32, false);
        _rawImage = GetComponent<RawImage>();
        _rawImage.texture = _texture;

        // テクスチャを白で初期化
        Color[] pixels = new Color[_texture.width * _texture.height];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.white;
        }
        _texture.SetPixels(pixels);
        _texture.Apply();
    }
    public Vector2 ManualUpdate()
    {
        return _trasform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // スクリーン座標をローカル座標に変換
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)transform,
            eventData.position,
            eventData.pressEventCamera,
            out localPosition);

        // ローカル座標をテクスチャ座標に変換
        RectTransform rectTransform = (RectTransform)transform;
        Vector2 texturePosition = new Vector2(
            (localPosition.x + rectTransform.rect.width / 2),
            (localPosition.y + rectTransform.rect.height / 2)
        );

        // 中心を原点とした座標を_trasformに格納
        _trasform = localPosition;
        if (_isDragging)
        {
            DrawLine(_previousPosition, texturePosition);
        }
        else
        {
            DrawPoint(texturePosition);
            _isDragging = true;
        }

        _previousPosition = texturePosition;
        _texture.Apply();
        _rawImage.texture = _texture;
    }
    /// <summary>
    /// 前のフレームの位置と現在の位置を結ぶ線を描画するメソッド
    /// </summary>
    private void DrawLine(Vector2 from, Vector2 to)
    {
        float distance = Vector2.Distance(from, to);
        int steps = Mathf.RoundToInt(distance);

        for (int i = 0; i <= steps; i++)
        {
            float t = i / (float)steps;
            Vector2 position = Vector2.Lerp(from, to, t);
            DrawPoint(position);
        }
    }
    /// <summary>
    /// 点を描画するメソッド
    /// </summary>
    private void DrawPoint(Vector2 position)
    {
        int halfWidth = lineWidth / 2;
        for (int x = -halfWidth; x <= halfWidth; x++)
        {
            for (int y = -halfWidth; y <= halfWidth; y++)
            {
                int pixelX = (int)position.x + x;
                int pixelY = (int)position.y + y;
                if (pixelX >= 0 && pixelX < _texture.width && pixelY >= 0 && pixelY < _texture.height)
                {
                    _texture.SetPixel(pixelX, pixelY, Color.red);
                }
            }
        }
    }
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        _isDragging = false;
        _texture = new Texture2D((int)((RectTransform)transform).rect.width,
        (int)((RectTransform)transform).rect.height, TextureFormat.RGBA32, false);
        _rawImage.texture = _texture;
        // テクスチャを白で初期化
        Color[] pixels = new Color[_texture.width * _texture.height];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.white;
        }
        _texture.SetPixels(pixels);
        _texture.Apply();
    }
}
