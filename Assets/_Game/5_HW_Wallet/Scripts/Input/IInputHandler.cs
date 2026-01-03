using System;

public interface IInputHandler
{
    event Action<WalletItemType, int> AddCurrency;
    event Action<WalletItemType, int> RemoveCurrency;
    event Action Clear;
}
