using UnityEngine;

public class TestUI : MonoBehaviour
{
    [SerializeField] private TestSpirit testSpirit;

    public void OnHitButton()
    {
        Debug.Log("[UI] Hit 버튼 클릭됨");
        testSpirit.TakeDamage(10);
    }
}