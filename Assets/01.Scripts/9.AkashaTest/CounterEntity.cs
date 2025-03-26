using Akasha;
using UnityEngine;

// 게임 월드의 실제 존재로서 여러 기능(Part)을 포함할 수 있는 기본 단위입니다.
// 모델을 소유하고 있으며, Part와의 연결 지점을 제공합니다.
public class CounterEntity : BaseEntity
{
    public CounterModel Model { get; private set; } = new();

    protected override void OnPartAdded(BasePart part)
    {
        Debug.Log("<color=blue>[Entity]</color> 파트가 엔티티에 등록됨!");

        // Part가 추가되면 모델을 연결해줍니다.
        if (part is CounterPart counterPart)
        {
            counterPart.BindModel(Model);
        }
    }
}