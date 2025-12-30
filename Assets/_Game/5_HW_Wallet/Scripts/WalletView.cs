using System.Collections.Generic;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private RectTransform _walletViewInner;
    [SerializeField] private WalletItemView _walletItemPrefab;
    [SerializeField] private List<WalletItemViewDTO> _walletItems;

    private IWalletDataChangeSender _wallet;
    private Dictionary<WalletItemType, WalletItemView> _itemsViews = new Dictionary<WalletItemType, WalletItemView>();

    public void Initialize(IWalletDataChangeSender wallet)
    {
        _wallet = wallet;
        _wallet.WalletDataChanged += OnWalletDataChanged;

        foreach (Transform child in _walletViewInner)
            Destroy(child.gameObject);

        _itemsViews.Clear();
    }

    private void OnWalletDataChanged()
    {
        List<Currency> activeCurrencies = _wallet.GetCurrencyBy(currency => currency.Amount > 0);

        List<WalletItemType> keysToRemove = new List<WalletItemType>();

        foreach (var type in _itemsViews.Keys)
        {
            bool isActive = false;

            foreach (var currency in activeCurrencies)
            {
                if (type == currency.ItemType)
                {
                    isActive = true;
                    break;
                }
            }

            if (!isActive)
                keysToRemove.Add(type);
        }

        foreach (var key in keysToRemove)
        {
            Destroy(_itemsViews[key].gameObject);
            _itemsViews.Remove(key);
        }

        foreach (var data in activeCurrencies)
        {
            if (_itemsViews.ContainsKey(data.ItemType))
            {
                _itemsViews[data.ItemType].SetValue(data.Amount);
            }
            else
            {
                foreach (var item in _walletItems)
                {
                    if (item.ItemType == data.ItemType)
                    {
                        WalletItemView view = Instantiate(_walletItemPrefab, _walletViewInner);
                        view.Setup(item.Sprite, data.Amount);

                        _itemsViews[data.ItemType] = view;

                        break;
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (_wallet != null)
            _wallet.WalletDataChanged -= OnWalletDataChanged;
    }
}
