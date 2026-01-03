using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomBottonHighlighter : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _highlightedColor;

    [SerializeField] private Image _image;

    public void OnPointerDown(PointerEventData eventData) => _image.color = _highlightedColor;

    public void OnPointerUp(PointerEventData eventData) => _image.color = _normalColor;
}