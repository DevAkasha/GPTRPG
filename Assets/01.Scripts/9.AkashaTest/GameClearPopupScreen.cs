using Akasha;
using TMPro;
using UnityEngine;


// ✅ GameClearPopupScreen
// 게임 클리어 시 표시되는 팝업 화면입니다.
// Manager에서 직접 Opened()를 호출하여 출력합니다.
public class GameClearPopupScreen : BaseScreen
{
    public TextMeshProUGUI messageText;

    protected override void OnSetup()
    {
        Debug.Log("<color=red>[Popup]</color> OnSetup → 텍스트 메세지 Game Clear! 세팅");
        messageText.text = "Game Clear!";
    }

    public override void Refresh()
    {
        Debug.Log("<color=red>[Popup]</color> Refresh");
    }
}