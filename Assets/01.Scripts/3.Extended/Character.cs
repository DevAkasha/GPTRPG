using System;
using Akasha;

public class Character : BaseEntity
{
    // ▣ 기본 정보
    public RxVar<int> Gold { get; } = new(10000);
    public RxVar<string> Name { get; } = new("Akasha");
    public RxVar<int> Level { get; } = new(10);

    // ▣ 경험치 / 체력
    public RxVar<float> MaxExp { get; } = new(100f);
    public RxVar<float> CurExp { get; } = new(70f);
    public RxVar<int> MaxHP { get; } = new(100);
    public RxVar<int> CurHP { get; } = new(-1);

    // ▣ 기본 능력치
    public RxVar<int> BaseAttack { get; } = new(10);
    public RxVar<int> BaseDefence { get; } = new(20);
    public RxVar<int> BaseCritical { get; } = new(15);

    // ▣ 인벤토리
    public RxList<Item> Inventory { get; } = new();

    // ▣ 계산형 능력치 (RxExpr)
    public RxExpr<int> TotalAttack { get; private set; }
    public RxExpr<int> TotalDefence { get; private set; }
    public RxExpr<int> TotalCritical { get; private set; }

    public Character()
    {
        // ▣ RxExpr 초기화 (계산식 정의)
        TotalAttack = CreateTotalStatExpr(BaseAttack, item => item.AttackBonus);
        TotalDefence = CreateTotalStatExpr(BaseDefence, item => item.DefenceBonus);
        TotalCritical = CreateTotalStatExpr(BaseCritical, item => item.CriticalBonus);

        // ▣ 인벤토리 변경 시 RxExpr 강제 리프레시
        Inventory.OnChangedDetailed += _ =>
        {
            TotalAttack.Refresh();
            TotalDefence.Refresh();
            TotalCritical.Refresh();
        };
    }
    private RxExpr<int> CreateTotalStatExpr(RxVar<int> baseValue, Func<Item, int> bonusSelector)
    {
        return new RxExpr<int>(() =>
        {
            int sum = baseValue.Value;
            foreach (var item in Inventory)
            {
                if (item.IsEquipped.Value)
                    sum += bonusSelector(item);
            }
            return sum;
        });
    }

    // ▣ 아이템 제어 메서드
    public void AddItem(Item item) => Inventory.Add(item);
    public void Equip(Item item)
    {
        if (Inventory.Contains(item))
            item.IsEquipped.Value = true;
    }
    public void UnEquip(Item item)
    {
        if (Inventory.Contains(item))
            item.IsEquipped.Value = false;
    }
}