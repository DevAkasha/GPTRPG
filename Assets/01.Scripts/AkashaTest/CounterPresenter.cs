using UnityEngine;
using Akasha;

// ✅ Presenter
// 버튼 등 UI 입력을 Spirit에게 전달하는 상향식 이벤트 해석자입니다.
// 실제로는 RaiseCommand → Spirit.OnButtonClicked.Raise() 연결 구조입니다.
public class CounterPresenter : BasePresenter
{
    public CounterSpirit? spirit;

    public void RaiseCommand()
    {
        Debug.Log("<color=purple>[Presenter]</color> 버튼클릭 이벤트감지 커맨드 발행!");
        spirit?.OnButtonClicked.Raise(this);
    }

    protected override void SetupBindings()
    {
        Debug.Log("<color=purple>[Presenter]</color> SetupBindings");
    }

    protected override void TeardownBindings()
    {
        Debug.Log("<color=purple>[Presenter]</color> TeardownBindings");
    }
}