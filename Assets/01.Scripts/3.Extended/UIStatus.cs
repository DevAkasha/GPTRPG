using Akasha;
using TMPro;
using UnityEngine;

public class UIStatus : BaseScreen
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI mpText;
    [SerializeField] private TextMeshProUGUI atkText;

    public void SetCharacter(Character character)
    {
    }

    public override void Refresh() { }
}
