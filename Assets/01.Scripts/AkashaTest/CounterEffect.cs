using Akasha;
using UnityEngine;

// ✅ Indicator
// Actor의 하위에 존재하며, View와 달리 상태 기반으로 시각 효과 등을 제어합니다.
// 예: 카운트가 10 이상이면 이펙트 활성화 등
public class CounterEffect : BaseIndicator
{
    public override void Refresh() { }

    public void SetEffect(bool isActive)
    {
        Debug.Log($"<color=purple>[Effect]</color> SetEffect 호출됨 → 이팩트 활성화! {isActive}");
        Root.SetActive(isActive);
    }

    protected override void OnActivate()
    {
        Debug.Log("<color=purple>[Effect]</color> OnActivate");
    }

    protected override void OnDeactivate()
    {
        Debug.Log("<color=purple>[Effect]</color> OnDeactivate");
    }
}