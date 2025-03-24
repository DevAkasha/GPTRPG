using Akasha;
using UnityEngine;
using UnityEngine.UI;

// ✅ Screen
// 유저가 클릭하거나 입력할 수 있는 UI 화면입니다.
// 버튼 클릭 → Presenter에게 이벤트 전달
public class CounterScreen : BaseScreen
{
    public Button clickButton;
    public CounterPresenter presenter;

    protected override void OnSetup()
    {
        Debug.Log("<color=red>[Screen]</color>  OnSetup : 버튼클릭에 present커맨드 등록 세팅됨");
        // 버튼 클릭 시 Presenter에 명령 전달
        clickButton.onClick.AddListener(() => presenter?.RaiseCommand());
    }

    public override void Refresh() { }
}
