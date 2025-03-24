using UnityEngine;
using Akasha;

// ✅ Actor
// Entity의 상태를 UI(View, Effect)로 표현하거나 Indicator에 전달하는 표현자입니다.
// Reactive 상태를 구독하고 View를 실시간 갱신합니다.
public class CounterActor : BaseActor<CounterEntity>
{
    public CounterView? view;
    public CounterEffect? effect;

    public override void RefreshView()
    {
        Debug.Log("<color=purple>[Actor]</color> RefreshView() 호출됨!");
        if (Entity?.Model is { } model) // Try "var model = Entity.Model;" 가능하면 조건문 진입 불가능하면 패스
        {
            Debug.Log($"<color=purple>[Actor]</color> 이펙트를 호출하고 뷰를 다음값으로 갱신함!: {model.Count.Value}");
            // View와 Effect에 상태 전달
            view?.SetCount(model.Count.Value);
            effect?.SetEffect(model.Count.Value >= 10);
        }
    }

    protected override void OnActivate()
    {
        Debug.Log("<color=purple>[Actor]</color> OnActivate() 호출됨!");
        if (Entity?.Model is { } model)
        {
            // 상태 구독 + 현재 값 즉시 반영 (초기 View 갱신)
            RxBind.Bind(model.Count, OnCountChanged, this);
            Debug.Log($"<color=purple>[Actor]</color> 모델에 뷰갱신이 바인드됨! 초기값: {model.Count.Value}");
        }
    }

    protected override void OnDeactivate()
    {
        Debug.Log("<color=purple>[Actor]</color> OnDeactivate() 호출됨 → 모든 바인딩 해제");
        // 비활성화 시 모든 구독 해제
        RxBind.UnbindAll(this);
    }

    private void OnCountChanged(int value)
    {
        Debug.Log($"<color=purple>[Actor]</color> 카운트 변경됨! 뷰를 갱신하겠음!: {value}");
        RefreshView();
    }
}
