using System;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : IWalletData, IDisposable
{
    public event Action WalletDataChanged;

    private List<Currency> _currencies = new List<Currency>();

    private Game _game;
    private readonly InputButtonsHandler _inputButtonsHandler;

    public Wallet(Game game, InputButtonsHandler inputButtonsHandler)
    {
        _game = game;
        _inputButtonsHandler = inputButtonsHandler;
        _game.AddCurrency += OnAddCurrency;
        _game.RemoveCurrency += OnRemoveCurrency;

        _inputButtonsHandler.AddCurrency += OnAddCurrency;
        _inputButtonsHandler.RemoveCurrency += OnRemoveCurrency;
    }

    public IReadOnlyList<Currency> Currencies => _currencies;

    private void OnAddCurrency(WalletItemType walletItemType, int value)
    {
        if (value <= 0)
            return;

        bool found = false;

        for (int i = 0; i < _currencies.Count; i++)
        {
            if (_currencies[i].ItemType == walletItemType)
            {
                _currencies[i].AddAmount(value);

                found = true;

                break;
            }
        }

        if (!found)
            _currencies.Add(new Currency(walletItemType, value));

        InvokeWalletAction(walletItemType);
    }

    private void OnRemoveCurrency(WalletItemType walletItemType, int value)
    {
        if (value <= 0)
            return;

        for (int i = 0; i < _currencies.Count; i++)
        {
            if (_currencies[i].ItemType == walletItemType)
            {
                _currencies[i].RemoveAmount(value);

                if (_currencies[i].Amount <= 0)
                    _currencies[i].Clear();

                InvokeWalletAction(walletItemType);

                break;
            }
        }
    }

    private void Clear()
    {
        _currencies.Clear();

        WalletDataChanged?.Invoke();

        Debug.Log($"Кошелек очищен");
    }

    private void InvokeWalletAction(WalletItemType walletItemType)
    {
        WalletDataChanged?.Invoke();

        int currentAmount = 0;

        foreach (var currency in _currencies)
        {
            if (currency.ItemType == walletItemType)
            {
                currentAmount = currency.Amount;

                break;
            }
        }

        Debug.Log($"Изменилось количество - {walletItemType} : {currentAmount}");
    }

    public void Dispose()
    {
        _game.AddCurrency -= OnAddCurrency;
        _game.RemoveCurrency -= OnRemoveCurrency;

        _inputButtonsHandler.AddCurrency -= OnAddCurrency;
        _inputButtonsHandler.RemoveCurrency -= OnRemoveCurrency;
    }
}
