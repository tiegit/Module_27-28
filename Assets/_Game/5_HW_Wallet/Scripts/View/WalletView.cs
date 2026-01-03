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
            bool isActive = activeCurrencies.Exists(currency => type == currency.ItemType);

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
                        WalletItemView itemView = Instantiate(_walletItemPrefab, _walletViewInner);
                        itemView.Setup(item.Sprite, data.Amount);

                        _itemsViews[data.ItemType] = itemView;

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
