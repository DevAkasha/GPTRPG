using Akasha;
using TMPro;
using UnityEngine;

// ✅ View
// 상태를 UI로 표현하는 시각적 컴포넌트입니다.
// ReactiveField의 값을 받아 텍스트 등으로 출력합니다.
public class CounterView : BaseView
{
    public TextMeshProUGUI countText;

    private int _currentValue;

    public void SetCount(int value)
    {
        Debug.Log($"<color=red>[View]</color> SetCount: {value}");
        _currentValue = value;
        Refresh();
    }

    public override void Refresh()
    {
        Debug.Log($"<color=red>[View]</color> Refresh called with count: {_currentValue}");
        countText.text = $"Count: {_currentValue}";
    }
}
