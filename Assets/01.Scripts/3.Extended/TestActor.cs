using Akasha;
using UnityEngine;
using UnityEngine.UI;

public class TestActor : BaseActor<TestEntity>
{
    [SerializeField] private Slider hpSlider;

    public override void RefreshView()
    {
        if (Entity == null || hpSlider == null)
        {
            Debug.LogWarning("[Actor] Entity 또는 Slider가 null입니다.");
            return;
        }

        Debug.Log($"[Actor] RefreshView: {Entity.Hp.Value}");
        hpSlider.value = Entity.Hp.Value / 100f;
    }

    protected override void OnActivate()
    {
        if (Entity == null) return;

        ReactiveBinder.Bind(Entity.Hp, OnHpChanged, this);
    }

    protected override void OnDeactivate()
    {
        ReactiveBinder.UnbindAll(this);
    }
    private void OnHpChanged(int newHp)
    {
        Debug.Log($"[Actor] HP 변경 감지 → View 갱신: {newHp}");
        RefreshView();
    }
}