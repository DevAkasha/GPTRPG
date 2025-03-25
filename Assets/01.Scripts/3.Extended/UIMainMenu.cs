using Akasha;
using TMPro;
using UnityEngine;

public class UIMainMenu : BaseScreen
{
    [SerializeField] private TextMeshProUGUI nameText;

    public void SetCharacter(Character character)
    {
        nameText.text = $"Name: {character.Name}";
    }

    public override void Refresh() { }
}
