using Akasha;

public class TestEntity : BaseEntity
{
    public ReactiveProperty<int> Hp { get; private set; } = new(100);

    public override void OnInitialize()
    {
        base.OnInitialize();
        // 초기 설정 가능
    }
}