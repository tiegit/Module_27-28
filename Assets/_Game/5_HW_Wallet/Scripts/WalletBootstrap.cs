using UnityEngine;

public class WalletBootstrap : MonoBehaviour
{
    [SerializeField] private WalletView _walletView;
    [SerializeField] private InputButtonsHandler _inputButtonsHandler;
    private Wallet _wallet;
    private Game _game;

    private void Awake()
    {
        PlayerInput playerInput = new PlayerInput();

        _game = new Game(playerInput);

        _wallet = new Wallet(_game, _inputButtonsHandler);

        if (_walletView != null)
            _walletView.Initialize(_wallet);
    }

    private void Update()
    {
        _game.CustomUpdate();
    }

    private void OnDestroy()
    {
        _wallet.Dispose();
    }
}
