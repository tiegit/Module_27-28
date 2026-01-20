using System;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : IWallet
{
    public event Action<WalletItemType, int> WalletDataChanged;
    public event Action WalletCleared;

    private List<Currency> _currencies = new List<Currency>();

    public void AddCurrency(WalletItemType walletItemType, int value)
    {
        if (value <= 0)
            return;

        var currencies = GetCurrencyBy(c => c.ItemType == walletItemType);

        if (currencies.Count > 0)
        {
            currencies[0].AddAmount(value);
            InvokeWalletAction(walletItemType, currencies[0].Amount);
        }
        else
        {
            _currencies.Add(new Currency(walletItemType, value));
            InvokeWalletAction(walletItemType, value);
        }
    }

    public bool CanRemoveCurrency(WalletItemType walletItemType, int value)
    {
        if (value <= 0)
            return false;

        var currencies = GetCurrencyBy(c => c.ItemType == walletItemType);

        if (currencies.Count == 0)
            return false;

        Currency currency = currencies[0];
        return currency.Amount >= value;
    }

    public void RemoveCurrency(WalletItemType walletItemType, int value)
    {
        if (value <= 0)
            return;

        var currencies = GetCurrencyBy(c => c.ItemType == walletItemType);

        if (currencies.Count > 0)
        {
            Currency currency = currencies[0];

            currency.RemoveAmount(value);

            InvokeWalletAction(walletItemType, currency.Amount);

            if (currency.Amount <= 0)
                _currencies.Remove(currency);
        }
    }

    public void Clear()
    {
        _currencies.Clear();

        WalletCleared?.Invoke();

        Debug.Log($"Кошелек очищен");
    }

    public List<Currency> GetCurrencyBy(Func<Currency, bool> itemFilter)
    {
        List<Currency> selectedCurrency = new List<Currency>();

        foreach (Currency item in _currencies)
        {
            if (itemFilter != null && itemFilter.Invoke(item))
                selectedCurrency.Add(item);
        }

        return selectedCurrency;
    }

    private void InvokeWalletAction(WalletItemType walletItemType, int amount)
    {
        WalletDataChanged?.Invoke(walletItemType, amount);

        Debug.Log($"Изменилось количество - {walletItemType} : {amount}");
    }
}
