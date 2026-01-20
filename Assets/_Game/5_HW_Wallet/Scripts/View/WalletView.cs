using System.Collections.Generic;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private RectTransform _walletViewInner;
    [SerializeField] private WalletItemView _walletItemPrefab;
    [SerializeField] private List<WalletItem> _walletItems;

    private IWallet _wallet;
    private Dictionary<WalletItemType, WalletItemView> _itemsViews = new Dictionary<WalletItemType, WalletItemView>();

    public void Initialize(IWallet wallet)
    {
        _wallet = wallet;
        _wallet.WalletDataChanged += OnWalletDataChanged;
        _wallet.WalletCleared += OnWalletCleared;

        foreach (Transform child in _walletViewInner)
            Destroy(child.gameObject);

        _itemsViews.Clear();
    }

    private void OnWalletDataChanged(WalletItemType walletItemType, int amount)
    {
        if (amount <= 0)
        {
            Destroy(_itemsViews[walletItemType].gameObject);
            _itemsViews.Remove(walletItemType);

            return;
        }

        if (_itemsViews.ContainsKey(walletItemType))
        {
            _itemsViews[walletItemType].SetValue(amount);
        }
        else
        {
            foreach (var item in _walletItems)
            {
                if (item.ItemType == walletItemType)
                {
                    WalletItemView itemView = Instantiate(_walletItemPrefab, _walletViewInner);
                    itemView.Setup(item.Sprite, amount);

                    _itemsViews[walletItemType] = itemView;

                    break;
                }
            }
        }
    }

    private void OnWalletCleared()
    {
        var keysToRemove = new List<WalletItemType>(_itemsViews.Keys);

        foreach (var key in keysToRemove)
        {
            var itemView = _itemsViews[key];

            Destroy(itemView.gameObject);

            _itemsViews.Remove(key);
        }
    }

    private void OnDestroy()
    {
        if (_wallet != null)
        {
            _wallet.WalletDataChanged -= OnWalletDataChanged;
            _wallet.WalletCleared -= OnWalletCleared;
        }
    }
}
