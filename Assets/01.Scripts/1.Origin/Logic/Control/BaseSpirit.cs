using UnityEngine;

namespace Akasha
{
    /// <summary>
    /// Spirit은 Entity와 Actor를 함께 제어하는 게임 로직의 중심체입니다.
    /// 상향식 이벤트를 해석하여 Entity와 Actor에 명령을 전달합니다.
    /// </summary>
    public abstract class BaseSpirit : MonoBehaviour, IControlLogicalSubscriber { }

    /// <summary>
    /// 특정 Entity와 Actor를 제어하는 Spirit.
    /// 외부에서 의존성을 주입받거나, 자동으로 GetComponent를 통해 연결됩니다.
    /// </summary>
    public abstract class BaseSpirit<TEntity> : BaseSpirit
        where TEntity : BaseEntity
    {
        [SerializeField] private TEntity? entity;
        [SerializeField] private BaseActor? actor;

        public TEntity? Entity => entity;
        public BaseActor? Actor => actor;

        /// <summary>
        /// 외부에서 Entity를 수동 주입합니다.
        /// </summary>
        public void InjectEntity(TEntity entity)
        {
            this.entity = entity;
            OnEntityInjected();
        }

        /// <summary>
        /// 외부에서 Actor를 수동 주입합니다.
        /// </summary>
        public void InjectActor(BaseActor actor)
        {
            this.actor = actor;
            OnActorInjected();
        }

        /// <summary>
        /// Unity Editor 상에서 연결 확인 및 자동 연동을 수행합니다.
        /// </summary>
        protected virtual void OnValidate()
        {
            if (entity == null) entity = GetComponent<TEntity>();
            if (actor == null) actor = GetComponent<BaseActor>();

            if (entity != null) OnEntityInjected();
            if (actor != null) OnActorInjected();
        }

        /// <summary>
        /// 런타임 초기화 시 자동 연동 및 설정을 수행합니다.
        /// </summary>
        protected virtual void Awake()
        {
            if (entity == null) entity = GetComponent<TEntity>();
            if (actor == null) actor = GetComponent<BaseActor>();

            OnSetup();
        }

        /// <summary>
        /// 초기 설정이 필요한 경우 오버라이드합니다.
        /// </summary>
        protected virtual void OnSetup() { }

        /// <summary>
        /// Entity 주입 직후 호출됩니다.
        /// </summary>
        protected virtual void OnEntityInjected() { }

        /// <summary>
        /// Actor 주입 직후 호출됩니다.
        /// </summary>
        protected virtual void OnActorInjected() { }

        /// <summary>
        /// Spirit 활성화 시 호출됩니다.
        /// ReactiveField와의 연동은 하지 않습니다.
        /// </summary>
        protected virtual void OnEnable() => OnActivate();

        /// <summary>
        /// Spirit 비활성화 시 호출됩니다.
        /// </summary>
        protected virtual void OnDisable() => OnDeactivate();

        protected abstract void OnActivate();
        protected abstract void OnDeactivate();
    }
}