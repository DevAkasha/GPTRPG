using Akasha;
using UnityEngine;

// ✅ Part
// Entity 내부의 기능적 모듈로, 상태를 계산하거나 실질적 기능을 구현 합니다.
public class CounterPart : BasePart
{
    private CounterModel? _model;

    public void BindModel(CounterModel model)
    {
        Debug.Log("<color=blue>[Part]</color> 파트에 모델이 바인드됨!");
        _model = model;
        // 상태 구독: 값이 변경될 때마다 OnCountChanged 호출
        model.Count.Subscribe(OnCountChanged, this, RxType.Functional);
    }

    private void OnCountChanged(int value)
    {
        Debug.Log($"<color=blue>[Part]</color> 모델의 카운트가 변경됨! {value}");
    }
}

