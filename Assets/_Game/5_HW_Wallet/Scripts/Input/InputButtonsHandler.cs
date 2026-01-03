using System;
using UnityEngine;
using UnityEngine.UI;

public class InputButtonsHandler : MonoBehaviour
{
    public event Action<WalletItemType, int> AddCurrency;
    public event Action<WalletItemType, int> RemoveCurrency;

    [SerializeField] private Button _coinAddButton;
    [SerializeField] private Button _coinRemoveButton;
    [SerializeField] private Button _diamondAddButton;
    [SerializeField] private Button _diamondRemoveButton;
    [SerializeField] private Button _energyAddButton;
    [SerializeField] private Button _energyRemoveButton;

    private void OnEnable()
    {
        _coinAddButton.onClick.AddListener(() => AddCurrencyRequest(WalletItemType.Coin, 1));
        _coinRemoveButton.onClick.AddListener(() => RemoveCurrencyRequest(WalletItemType.Coin, 1));

        _diamondAddButton.onClick.AddListener(() => AddCurrencyRequest(WalletItemType.Diamond, 1));
        _diamondRemoveButton.onClick.AddListener(() => RemoveCurrencyRequest(WalletItemType.Diamond, 1));

        _energyAddButton.onClick.AddListener(() => AddCurrencyRequest(WalletItemType.Energy, 1));
        _energyRemoveButton.onClick.AddListener(() => RemoveCurrencyRequest(WalletItemType.Energy, 1));
    }

    private void OnDisable()
    {
        _coinAddButton.onClick.RemoveAllListeners();
        _coinRemoveButton.onClick.RemoveAllListeners();
        _diamondAddButton.onClick.RemoveAllListeners();
        _diamondRemoveButton.onClick.RemoveAllListeners();
        _energyAddButton.onClick.RemoveAllListeners();
        _energyRemoveButton.onClick.RemoveAllListeners();
    }

    private void AddCurrencyRequest(WalletItemType walletItemType, int value) => AddCurrency?.Invoke(walletItemType, value);

    private void RemoveCurrencyRequest(WalletItemType walletItemType, int value) => RemoveCurrency?.Invoke(walletItemType, value);
}
