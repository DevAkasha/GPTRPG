using System.Collections.Generic;
using System.Linq;
using Akasha;
using UnityEngine;


public class EGameManager : Manager<EGameManager>
{
    public Character player;
    private void Update()
    {
        RxQueue.Flush();
    }
    protected override void OnActivate() => SetData();

    protected override void OnDeactivate() { }

    private void SetData()
    {
        var potion = new Item("Health Potion");
        var mana = new Item("Mana Potion");
        var sword = new Item("Wood Sword", atk: 10);
      
        player.AddItem(potion);
        player.AddItem(mana);
        player.AddItem(sword);
        player.Equip(sword);
        Debug.Log($"[GameManager] 인벤토리에 {player.Inventory.Count}개 아이템 추가 완료");
    }
}