using UnityEngine;

public class WalletBootstrap : MonoBehaviour
{
    [SerializeField] private WalletView _walletView;
    [SerializeField] private InputButtonsHandler _inputButtonsHandler;

    private Wallet _wallet;
    private InputHandler _inputHandler;
    private CombinedInputHandler _combinedInputHandler;

    private void Awake()
    {
        PlayerInput playerInput = new PlayerInput();
        _inputHandler = new InputHandler(playerInput);

        _combinedInputHandler = new CombinedInputHandler(_inputHandler, _inputButtonsHandler);

        _wallet = new Wallet(_combinedInputHandler);

        if (_walletView != null)
            _walletView.Initialize(_wallet);
    }

    private void Update()
    {
        _inputHandler.CustomUpdate();
    }

    private void OnDestroy()
    {
        _wallet.Dispose();
        _combinedInputHandler.Dispose();
    }
}
