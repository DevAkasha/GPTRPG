using Akasha;
using TMPro;
using UnityEngine;

public class StatWidget : BaseWidget 
{
    //변수 선언
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI defenceText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI criticalText;
    protected override void OnSetup()
    {
        var player = EGameManager.Instance.player;
        //연결 선언
        RxBind.Bind(player.TotalAttack, _ => Refresh(), this, RxType.Functional);
        RxBind.Bind(player.TotalDefence, _ => Refresh(), this, RxType.Functional);
        RxBind.Bind(player.MaxHP, _ => Refresh(), this, RxType.Functional);
        RxBind.Bind(player.TotalCritical, _ => Refresh(), this, RxType.Functional);
    }

    public override void Refresh()
    {
        var player = EGameManager.Instance.player;
        //동작 선언
        attackText.text = $"attack: {player.TotalAttack.Value}";
        defenceText.text = $"defence: {player.TotalDefence.Value}";
        healthText.text = $"health: {player.MaxHP.Value}";
        criticalText.text = $"critical: {player.TotalCritical.Value}";
    }

    public override void HideDirect()
    {
        throw new System.NotImplementedException();
    }

    public override void Opened(params object[] param)
    {
        throw new System.NotImplementedException();
    }

}