using System;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : IWalletDataChangeSender, IDisposable
{
    public event Action WalletDataChanged;

    private List<Currency> _currencies = new List<Currency>();
    private IInputHandler _inputHandler;

    public Wallet(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;

        _inputHandler.AddCurrency += OnAddCurrency;
        _inputHandler.RemoveCurrency += OnRemoveCurrency;
        _inputHandler.Clear += OnClear;
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

    private void OnAddCurrency(WalletItemType walletItemType, int value)
    {
        if (value <= 0)
            return;

        var currencies = GetCurrencyBy(c => c.ItemType == walletItemType);

        if (currencies.Count > 0)
            currencies[0].AddAmount(value);
        else
            _currencies.Add(new Currency(walletItemType, value));

        InvokeWalletAction(walletItemType);
    }

    private void OnRemoveCurrency(WalletItemType walletItemType, int value)
    {
        if (value <= 0)
            return;

        var currencies = GetCurrencyBy(currency => currency.ItemType == walletItemType);

        if (currencies.Count > 0)
        {
            Currency currency = currencies[0];
            currency.RemoveAmount(value);

            if (currency.Amount <= 0)
                currency.Clear();

            InvokeWalletAction(walletItemType);
        }
    }

    private void OnClear()
    {
        _currencies.Clear();

        WalletDataChanged?.Invoke();

        Debug.Log($"Кошелек очищен");
    }

    private void InvokeWalletAction(WalletItemType walletItemType)
    {
        WalletDataChanged?.Invoke();

        int currentAmount = 0;

        var currencies = GetCurrencyBy(currency => currency.ItemType == walletItemType);

        if (currencies.Count > 0)
            currentAmount = currencies[0].Amount;

        Debug.Log($"Изменилось количество - {walletItemType} : {currentAmount}");
    }

    public void Dispose()
    {
        _inputHandler.AddCurrency -= OnAddCurrency;
        _inputHandler.RemoveCurrency -= OnRemoveCurrency;
        _inputHandler.Clear -= OnClear;
    }
}
