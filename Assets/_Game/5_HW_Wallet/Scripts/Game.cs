using System;

public class Game
{
    public event Action<WalletItemType, int> AddCurrency;
    public event Action<WalletItemType, int> RemoveCurrency;
    public event Action Clear;

    private PlayerInput _playerInput;

    public Game(PlayerInput playerInput) => _playerInput = playerInput;

    public void CustomUpdate()
    {
        if (_playerInput.A1KeyPressed)
            AddCurrency?.Invoke(WalletItemType.Coin, 1);

        if (_playerInput.A2KeyPressed)
            AddCurrency?.Invoke(WalletItemType.Diamond, 1);

        if (_playerInput.A3KeyPressed)
            AddCurrency?.Invoke(WalletItemType.Energy, 1);

        if (_playerInput.A4KeyPressed)
            RemoveCurrency?.Invoke(WalletItemType.Coin, 10);

        if (_playerInput.A5KeyPressed)
            RemoveCurrency?.Invoke(WalletItemType.Diamond, 1);

        if (_playerInput.A6KeyPressed)
            RemoveCurrency?.Invoke(WalletItemType.Energy, 1);

        if (_playerInput.CKeyPressed)
            Clear?.Invoke();
    }
}