using System;

public interface IWallet
{
    event Action<WalletItemType, int> WalletDataChanged;
    event Action WalletCleared;
}