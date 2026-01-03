using System;

public class CombinedInputHandler : IInputHandler, IDisposable
{
    public event Action<WalletItemType, int> AddCurrency;
    public event Action<WalletItemType, int> RemoveCurrency;
    public event Action Clear;

    private InputHandler _inputHandler;
    private InputButtonsHandler _inputButtonsHandler;

    public CombinedInputHandler(InputHandler inputHandler, InputButtonsHandler inputButtonsHandler)
    {
        _inputHandler = inputHandler;
        _inputButtonsHandler = inputButtonsHandler;

        _inputHandler.AddCurrency += OnAddCurrency;
        _inputHandler.RemoveCurrency += OnRemoveCurrency;
        _inputHandler.Clear += OnClear;

        _inputButtonsHandler.AddCurrency += OnAddCurrency;
        _inputButtonsHandler.RemoveCurrency += OnRemoveCurrency;
    }

    private void OnAddCurrency(WalletItemType type, int value) => AddCurrency?.Invoke(type, value);
    private void OnRemoveCurrency(WalletItemType type, int value) => RemoveCurrency?.Invoke(type, value);
    private void OnClear() => Clear?.Invoke();

    public void Dispose()
    {
        _inputHandler.AddCurrency -= OnAddCurrency;
        _inputHandler.RemoveCurrency -= OnRemoveCurrency;
        _inputHandler.Clear -= OnClear;

        _inputButtonsHandler.AddCurrency -= OnAddCurrency;
        _inputButtonsHandler.RemoveCurrency -= OnRemoveCurrency;
    }
}
