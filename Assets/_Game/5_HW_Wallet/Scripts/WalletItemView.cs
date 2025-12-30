using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WalletItemView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _currencyValue;

    public void Setup(Sprite sprite, int startValue)
    {
        SetImage(sprite);
        SetValue(startValue);
    }

    public void SetValue(int value)
    {
        if (_currencyValue != null)
            _currencyValue.text = value.ToString();
    }

    private void SetImage(Sprite sprite)
    {
        if (_image != null)
            _image.sprite = sprite;
    }
}