using Akasha;
using UnityEngine;
using UnityEngine.UI;

public class RightPanelPresenter : BasePresenter
{
    [Header("버튼")]
    [SerializeField] private Button backButton;
    [SerializeField] private Button statusButton;
    [SerializeField] private Button inventoryButton;

    [Header("패널")]
    [SerializeField] private GameObject switchButtons;
    [SerializeField] private GameObject panelStatus;
    [SerializeField] private GameObject panelInventory;
    private void Start()
    {
        ShowSwitchButtons();
    }
    protected override void SetupBindings()
    {
        backButton.onClick.AddListener(ShowSwitchButtons);
        statusButton.onClick.AddListener(ShowStatusPanel);
        inventoryButton.onClick.AddListener(ShowInventoryPanel);
    }

    protected override void TeardownBindings()
    {
        backButton.onClick.RemoveAllListeners();
        statusButton.onClick.RemoveAllListeners();
        inventoryButton.onClick.RemoveAllListeners();
    }

    private void ShowSwitchButtons()
    {
        switchButtons.SetActive(true);
        panelStatus.SetActive(false);
        panelInventory.SetActive(false);
    }

    private void ShowStatusPanel()
    {
        switchButtons.SetActive(false);
        panelStatus.SetActive(true);
        panelInventory.SetActive(false);
    }

    private void ShowInventoryPanel()
    {
        switchButtons.SetActive(false);
        panelStatus.SetActive(false);
        panelInventory.SetActive(true);
    }
}