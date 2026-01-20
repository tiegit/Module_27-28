using System;
using UnityEngine;
using UnityEngine.UI;

public class InputButtons : MonoBehaviour
{
    public event Action AddCoin;
    public event Action RemoveCoin;
    public event Action AddDiamond;
    public event Action RemoveDiamond;
    public event Action AddEnergy;
    public event Action RemoveEnergy;

    [SerializeField] private Button _coinAddButton;
    [SerializeField] private Button _coinRemoveButton;
    [SerializeField] private Button _diamondAddButton;
    [SerializeField] private Button _diamondRemoveButton;
    [SerializeField] private Button _energyAddButton;
    [SerializeField] private Button _energyRemoveButton;

    private void OnEnable()
    {
        _coinAddButton.onClick.AddListener(AddCoinClicked);
        _coinRemoveButton.onClick.AddListener(RemoveCoinClicked);

        _diamondAddButton.onClick.AddListener(AddDiamondClicked);
        _diamondRemoveButton.onClick.AddListener(RemoveDiamondClicked);

        _energyAddButton.onClick.AddListener(AddEnergyClicked);
        _energyRemoveButton.onClick.AddListener(RemoveEnergyClicked);
    }

    private void OnDisable()
    {
        _coinAddButton.onClick.RemoveListener(AddCoinClicked);
        _coinRemoveButton.onClick.RemoveListener(RemoveCoinClicked);

        _diamondAddButton.onClick.RemoveListener(AddDiamondClicked);
        _diamondRemoveButton.onClick.RemoveListener(RemoveDiamondClicked);

        _energyAddButton.onClick.RemoveListener(AddEnergyClicked);
        _energyRemoveButton.onClick.RemoveListener(RemoveEnergyClicked);
    }

    private void AddCoinClicked() => AddCoin?.Invoke();

    private void RemoveCoinClicked() => RemoveCoin?.Invoke();

    private void AddDiamondClicked() => AddDiamond?.Invoke();

    private void RemoveDiamondClicked() => RemoveDiamond?.Invoke();

    private void AddEnergyClicked() => AddEnergy?.Invoke();

    private void RemoveEnergyClicked() => RemoveEnergy?.Invoke();
}
