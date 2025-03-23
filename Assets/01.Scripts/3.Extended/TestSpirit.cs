using Akasha;
using UnityEngine;

public class TestSpirit : BaseSpirit<TestEntity>
{
    public void TakeDamage(int damage)
    {
        Debug.Log($"[Spirit] 피해 {damage}만큼 받음");

        if (Entity != null)
        {
            Entity.Hp.Value = Mathf.Max(0, Entity.Hp.Value - damage);
            Debug.Log($"[Spirit] HP 갱신됨: {Entity.Hp.Value}");
        }
    }
    protected override void Awake()
    {
        base.Awake();

        if (Entity == null)
            InjectEntity(GetComponent<TestEntity>());

        if (Actor == null)
            InjectActor(GetComponent<TestActor>());
    }
    private void Update()
    {
        ReactiveScheduler.Flush();
    }
    protected override void OnEntityInjected()
    {
        // Reactive 구독을 여기서 해도 되고, OnActivate에서 해도 됨
    }

    protected override void OnActivate()
    {
    }

    protected override void OnDeactivate()
    {
        ReactiveBinder.UnbindAll(this);
    }
}