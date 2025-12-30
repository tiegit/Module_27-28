using System.Collections.Generic;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private RectTransform _walletViewInner;
    [SerializeField] private WalletItemView _walletItemPrefab;
    [SerializeField] private List<WalletItemViewDTO> _walletItems;

    private Wallet _wallet;
    private Dictionary<WalletItemType, WalletItemView> _itemsViews = new Dictionary<WalletItemType, WalletItemView>();

    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;
        _wallet.WalletDataChanged += OnWalletDataChanged;

        foreach (Transform child in _walletViewInner)
            Destroy(child.gameObject);

        _itemsViews.Clear();
    }

    private void OnWalletDataChanged() => SyncItems();

    private void SyncItems()
    {
        RemoveInactiveViews();

        foreach (var data in _wallet.Currencies)
        {
            if (data.Amount <= 0)
                continue;

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

    private void RemoveInactiveViews()
    {
        var keysToRemove = new List<WalletItemType>();

        foreach (var type in _itemsViews.Keys)
        {
            bool isActive = false;

            foreach (var currency in _wallet.Currencies)
            {
                if (currency.ItemType == type && currency.Amount > 0)
                {
                    isActive = true;
                    break;
                }
            }

            if (!isActive)
            {
                Destroy(_itemsViews[type].gameObject);
                keysToRemove.Add(type);
            }
        }

        foreach (var type in keysToRemove)
            _itemsViews.Remove(type);
    }

    private void OnDestroy()
    {
        if (_wallet != null)
            _wallet.WalletDataChanged -= OnWalletDataChanged;
    }
}
