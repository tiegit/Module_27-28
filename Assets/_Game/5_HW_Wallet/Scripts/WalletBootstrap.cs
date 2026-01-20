using UnityEngine;

public class WalletBootstrap : MonoBehaviour
{
    [SerializeField] private WalletView _walletView;
    [SerializeField] private InputButtons _inputButtons;

    private Wallet _wallet;
    private Player _player;

    private void Awake()
    {
        PlayerInput playerInput = new PlayerInput();

        _wallet = new Wallet();

        _player = new Player(playerInput, _inputButtons, _wallet);

        if (_walletView != null)
            _walletView.Initialize(_wallet);
    }

    private void Update()
    {
        _player.CustomUpdate();
    }

    private void OnDestroy()
    {
        _player.Dispose();
    }
}
