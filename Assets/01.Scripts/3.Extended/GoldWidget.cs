using Akasha;
using TMPro;
using UnityEngine;

public class GoldWidget : BaseWidget
{
    [SerializeField] private TextMeshProUGUI goldText;

    public override void HideDirect()
    {
        throw new System.NotImplementedException();
    }

    public override void Opened(params object[] param)
    {
        throw new System.NotImplementedException();
    }

    public override void Refresh()
    {
        var player = EGameManager.Instance.player;
        goldText.text = $"{player.Gold.Value:N0}";
    }

    protected override void OnSetup()
    {
        var player = EGameManager.Instance.player;
        RxBind.Bind(player.Gold, _ => Refresh(), this, RxType.Functional);
    }
}