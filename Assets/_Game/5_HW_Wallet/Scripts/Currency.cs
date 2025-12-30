public class Currency
{
    private WalletItemType _itemType;
    private int _amount;

    public Currency(WalletItemType itemType, int amount)
    {
        _itemType = itemType;
        _amount = amount;
    }

    public WalletItemType ItemType => _itemType;
    public int Amount 
    { 
        get => _amount; 
        private set
        {
            _amount = value;

            if (_amount < 0)
                _amount = 0;
        }
    }

    public void AddAmount(int value) => _amount += value;

    public void RemoveAmount(int value) => _amount -= value;

    public void Clear() => _amount = 0;
}