using UnityEngine;
using Akasha;

// ✅ Spirit
// 상향식 이벤트(Command)를 받아서 Entity에 명령을 내리는 게임 로직 중심체입니다.
// Presenter나 Actor로부터 명령을 받고, 상태를 갱신하거나 이벤트를 발생시킵니다.
public class CounterSpirit : BaseSpirit<CounterEntity>
{
    // 버튼 클릭 시 발생하는 ReactiveCommand입니다.
    public RxEvent OnButtonClicked = new();

    protected override void OnSetup()
    {
        Debug.Log("<color=orange>[Spirit]</color> OnSetup → 버튼 온클릭 구독");
        OnButtonClicked.Subscribe(HandleCommand, this);
    }

    public void HandleCommand()
    {
        Debug.Log("<color=orange>[Spirit]</color> HandleCommand() 활성화");
        if (Entity?.Model is { } model)
        {
            model.Count.Value++;
            Debug.Log($"<color=orange>[Spirit]</color> Count {model.Count.Value}로 증가를 명령");

            // 카운트가 20이 되면 게임 클리어 이벤트 발생
            if (model.Count.Value == 20)
            {
                Debug.Log("<color=orange>[Spirit]</color> Count 20이면 → GameClearEvent발행!");
                GameEventManager.Instance.GameClearEvent.Raise(this);
            }
        }
    }

    protected override void OnActivate()
    {
        Debug.Log("<color=orange>[Spirit]</color> OnActivate");
    }

    protected override void OnDeactivate()
    {
        Debug.Log("<color=orange>[Spirit]</color> OnDeactivate");
    }
}
