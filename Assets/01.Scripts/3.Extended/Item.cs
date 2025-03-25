using Akasha;

public class Item
{
    public string Name { get; private set; }
    public RxVar<bool> IsEquipped { get; } = new(false);

    public int AttackBonus { get; private set; }
    public int DefenceBonus { get; private set; }
    public int CriticalBonus { get; private set; }

    public Item(string name, int atk = 0, int def = 0, int crit = 0)
    {
        Name = name;
        AttackBonus = atk;
        DefenceBonus = def;
        CriticalBonus = crit;
    }

    public void Equip() => IsEquipped.Value = true;
    public void UnEquip() => IsEquipped.Value = false;
}