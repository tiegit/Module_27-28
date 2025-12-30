using System;
using System.Collections.Generic;

public interface IWalletDataChangeSender
{
    event Action WalletDataChanged;

    List<Currency> GetCurrencyBy(Func<Currency, bool> itemFilter);
}