using Akasha;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : BaseScreen
{
    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private int slotCount = 120;

    private readonly List<UISlot> slots = new();

    public override void Refresh()
    {
        var player = EGameManager.Instance.player;
        UpdateInventoryUI(player.Inventory);
    }

    protected override void OnSetup()
    {
        var player = EGameManager.Instance.player;
        player.Inventory.OnChanged += Refresh;

        InitializeFixedSlots();

        player.Inventory.OnChanged += Refresh;

        Refresh();
    }

    private void InitializeFixedSlots()
    {
        // 슬롯이 한 번도 만들어지지 않았다면 생성
        if (slots.Count > 0) return;

        for (int i = 0; i < slotCount; i++)
        {
            var go = Instantiate(slotPrefab, slotParent);
            var slot = go.GetComponent<UISlot>();
            
            if (slot == null)
            {
                Debug.LogError("[UIInventory] 프리팹에 UISlot 컴포넌트가 없습니다.");
                continue;
            }

            slots.Add(slot);
        }
    }

    private void UpdateInventoryUI(RxList<Item> inventory)
    {
        var player = EGameManager.Instance.player;

        for (int i = 0; i < slots.Count; i++)
        {
            if (i < inventory.Count)
            {
                var item = inventory[i];
                slots[i].SetItem(item, player);
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    public override void Opened(params object[] param)
    {
        base.Opened(param);
        Refresh(); // 패널 열릴 때 다시 갱신
    }
}