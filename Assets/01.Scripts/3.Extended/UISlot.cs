using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : BaseWidget
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private GameObject equippedMark;
    [SerializeField] private Button clickButton;

    private Item? item;
    private Character? owner;

    public void SetItem(Item item, Character owner)
    {
        this.item = item;
        this.owner = owner;

        clickButton.onClick.RemoveAllListeners();
        clickButton.onClick.AddListener(() =>
        {
            Debug.Log($"[UISlot] {item.Name} 클릭됨. 현재 장착 상태: {item.IsEquipped.Value}");

            if (item.IsEquipped.Value)
                owner.UnEquip(item);
            else
                owner.Equip(item);

            Refresh();
        });

        Refresh();
    }

    public void Clear()
    {
        item = null;
        owner = null;
        clickButton.onClick.RemoveAllListeners();
        Refresh();
    }

    public override void Refresh()
    {
        if (item == null)
        {
            itemNameText.text = "";
            equippedMark.SetActive(false);
        }
        else
        {
            itemNameText.text = item.Name;
            equippedMark.SetActive(item.IsEquipped.Value);
        }
    }
}