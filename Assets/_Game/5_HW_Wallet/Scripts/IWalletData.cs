using System;
using System.Collections.Generic;

public interface IWalletData
{
    event Action WalletDataChanged;
    IReadOnlyList<Currency> Currencies { get; }
}