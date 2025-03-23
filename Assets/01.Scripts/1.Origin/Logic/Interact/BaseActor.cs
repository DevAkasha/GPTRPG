using System.Collections.Generic;
using System.Linq;
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

            RegisterIndicators();
            base.Awake();
        }
    }

    /// <summary>
    /// Actor의 공통 베이스 클래스. Indicator 자동 등록, 갱신, 제어 기능을 포함한다.
    /// </summary>
    public abstract class BaseActor : MonoBehaviour, IInteractLogicalSubscriber
    {
        private readonly List<IIndicator> _indicators = new();

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

        /// <summary>
        /// Actor 하위에 존재하는 모든 Indicator를 자동 등록한다.
        /// </summary>
        protected virtual void RegisterIndicators()
        {
            _indicators.Clear();
            _indicators.AddRange(GetComponentsInChildren<IIndicator>(includeInactive: true));
        }

        /// <summary>
        /// 등록된 모든 Indicator를 Refresh() 호출로 갱신한다.
        /// </summary>
        public virtual void RefreshAllIndicators()
        {
            foreach (var indi in _indicators)
                indi.Refresh();
        }

        /// <summary>
        /// 특정 타입의 Indicator만 Refresh() 호출한다.
        /// </summary>
        public virtual void RefreshIndicator<T>() where T : class, IIndicator
        {
            foreach (var indi in _indicators)
            {
                if (indi is T typed)
                    typed.Refresh();
            }
        }

        /// <summary>
        /// 특정 타입의 Indicator를 활성화하거나 비활성화한다.
        /// </summary>
        public virtual void SetIndicatorActive<T>(bool isActive) where T : class, IIndicator
        {
            foreach (var indi in _indicators)
            {
                if (indi is T typed && typed is MonoBehaviour mb)
                    mb.gameObject.SetActive(isActive);
            }
        }

        /// <summary>
        /// 모든 Indicator를 한 번에 활성화하거나 비활성화한다.
        /// </summary>
        public virtual void SetAllIndicatorsActive(bool isActive)
        {
            foreach (var indi in _indicators)
            {
                if (indi is MonoBehaviour mb)
                    mb.gameObject.SetActive(isActive);
            }
        }
    }
}