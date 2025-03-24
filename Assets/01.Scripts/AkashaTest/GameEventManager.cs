using Akasha;
using UnityEngine;

// ✅ GameEventManager
// GameEvent를 전역으로 발생시키고 처리하는 싱글톤 관리자입니다.
// Spirit으로부터 GameClearEvent를 수신하고 팝업을 실행합니다.
public class GameEventManager : Manager<GameEventManager>
{
    public GameClearPopupScreen popup;
    public GameEvent GameClearEvent { get; private set; } = new();
    private void Update()
    {
        //프레임워크 아카샤 계산 호출 
        ReactiveScheduler.Flush();
    }
    protected override void OnActivate()
    {
        Debug.Log("<color=orange>[Manager]</color> OnActivate 실행! GameClearEvent구독함");
        GameClearEvent.Subscribe(OnGameClear, this);
    }

    protected override void OnDeactivate()
    {
        Debug.Log("<color=orange>[Manager]</color> OnDeactivate실행! GameClearEvent구독해제");
        GameClearEvent.Unsubscribe(OnGameClear);
    }

    private void OnGameClear()
    {
        Debug.Log("<color=orange>[Manager]</color> OnGameClear호출됨! 클리어 Popup 띄움!");
        popup.gameObject.SetActive(true);
    }
}
