using Akasha;
using UnityEngine;

public class EUIManager : Manager<EUIManager>
{
    [SerializeField] private UIMainMenu mainMenu;
    [SerializeField] private UIStatus statusUI;
    [SerializeField] private UIInventory inventoryUI;

    public UIMainMenu MainMenu => mainMenu;
    public UIStatus Status => statusUI;
    public UIInventory Inventory => inventoryUI;

    protected override void OnActivate() { }

    protected override void OnDeactivate() { }
}
