using System;
using UnityEngine;

public class Player : IDisposable
{
    private PlayerInput _playerInput;
    private readonly InputButtons _inputButtons;
    private readonly Wallet _wallet;

    public Player(PlayerInput playerInput, InputButtons inputButtons, Wallet wallet)
    {
        _playerInput = playerInput;
        _inputButtons = inputButtons;
        _wallet = wallet;

        _inputButtons.AddCoin += OnAddCoin;
        _inputButtons.RemoveCoin += OnRemoveCoin;
        _inputButtons.AddDiamond += OnAddDiamond;
        _inputButtons.RemoveDiamond += OnRemoveDiamond;
        _inputButtons.AddEnergy += OnAddEnergy;
        _inputButtons.RemoveEnergy += OnRemoveEnergy;
    }

    public void CustomUpdate()
    {
        if (_playerInput.A1KeyPressed)
            AddCurrency(WalletItemType.Coin, 1);

        if (_playerInput.A2KeyPressed)
            AddCurrency(WalletItemType.Diamond, 1);

        if (_playerInput.A3KeyPressed)
            AddCurrency(WalletItemType.Energy, 1);

        if (_playerInput.A4KeyPressed)
            TryRemoveCurrency(WalletItemType.Coin, 10);

        if (_playerInput.A5KeyPressed)
            TryRemoveCurrency(WalletItemType.Diamond, 1);

        if (_playerInput.A6KeyPressed)
            TryRemoveCurrency(WalletItemType.Energy, 1);

        if (_playerInput.CKeyPressed)
            _wallet.Clear();
    }

    private void OnAddCoin() => AddCurrency(WalletItemType.Coin, 1);

    private void OnRemoveCoin() => TryRemoveCurrency(WalletItemType.Coin, 1);

    private void OnAddDiamond() => AddCurrency(WalletItemType.Diamond, 1);

    private void OnRemoveDiamond() => TryRemoveCurrency(WalletItemType.Diamond, 1);

    private void OnAddEnergy() => AddCurrency(WalletItemType.Energy, 1);

    private void OnRemoveEnergy() => TryRemoveCurrency(WalletItemType.Energy, 1);

    private void AddCurrency(WalletItemType type, int value) => _wallet.AddCurrency(type, value);

    public void TryRemoveCurrency(WalletItemType itemType, int amount)
    {
        if (_wallet.CanRemoveCurrency(itemType, amount))
        {
            _wallet.RemoveCurrency(itemType, amount);
        }
        else
        {
            var currencies = _wallet.GetCurrencyBy(c => c.ItemType == itemType);

            if (currencies.Count > 0)
            {
                int currentAmount = currencies[0].Amount;

                Debug.LogWarning($"Недостаточно {itemType} для удаления. " +
                                 $"Имеется: {currentAmount}, запрошено: {amount}");
            }
            else
            {
                Debug.LogWarning($"В кошельке нет {itemType} для использования.");
            }
        }
    }

    public void Dispose()
    {
        if (_inputButtons == null)
            return;

        _inputButtons.AddCoin -= OnAddCoin;
        _inputButtons.RemoveCoin -= OnRemoveCoin;
        _inputButtons.AddDiamond -= OnAddDiamond;
        _inputButtons.RemoveDiamond -= OnRemoveDiamond;
        _inputButtons.AddEnergy -= OnAddEnergy;
        _inputButtons.RemoveEnergy -= OnRemoveEnergy;
    }
}
