using System.Collections.Generic;
using UnityEngine;

namespace Akasha
{
    /// <summary>
    /// Actor의 제네릭 상속 버전. Entity와 연결되어 상태 기반 View 갱신을 담당한다.
    /// </summary>
    public abstract class BaseActor<TEntity> : BaseActor
        where TEntity : BaseEntity
    {
        [SerializeField, Tooltip("이 Actor가 제어할 Entity")]
        private TEntity? entity;

        public TEntity? Entity => entity;

        public void InjectEntity(TEntity entity)
        {
            this.entity = entity;
            OnEntityInjected();
        }

        protected virtual void OnEntityInjected() { }

        protected override void Awake()
        {
            if (entity == null)
                entity = GetComponent<TEntity>();

            base.Awake();
        }
    }

    /// <summary>
    /// Actor의 공통 베이스 클래스. Indicator 자동 등록, 갱신, 제어 기능을 포함한다.
    /// </summary>
    public abstract class BaseActor : MonoBehaviour, IInteractLogicalSubscriber, IReactiveEventIssuer
    {
        public abstract void RefreshView();

        protected virtual void Awake()
        {
            OnSetup();
        }

        protected virtual void OnEnable() => OnActivate();
        protected virtual void OnDisable() => OnDeactivate();

        protected virtual void OnSetup() { }
        protected abstract void OnActivate();
        protected abstract void OnDeactivate();

    }
}