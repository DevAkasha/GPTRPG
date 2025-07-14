using UnityEngine;

namespace Akasha
{
    /// <summary>
    /// BasePart는 Entity의 내부 구성 단위로, 기능적 구분을 위한 서브 모듈입니다.
    /// Entity에 부착되거나 분리될 수 있으며, 필요한 경우 RxField를 직접 구독합니다.
    /// </summary>
    public abstract class BasePart : RxContextBehaviour, IFunctionalSubscriber
    {
        public BaseEntity? Entity { get; private set; }

        internal void SetParent(BaseEntity? entity)
        {
            Entity = entity;

            if (entity != null)
                OnAttachedToEntity(entity);
            else
                OnDetachedFromEntity();
        }

        protected override void OnInit()
        {
            base.OnInit();
            OnInitialize();
        }

        protected override void OnDispose()
        {
            OnTerminate();
            base.OnDispose();
        }

        /// <summary>
        /// 초기화 시 호출됩니다. RxField 바인딩은 여기서 수행 가능합니다.
        /// </summary>
        protected virtual void OnInitialize() { }

        /// <summary>
        /// 종료 시 호출됩니다. 수동 해제가 필요한 경우 사용합니다.
        /// </summary>
        protected virtual void OnTerminate() { }

        /// <summary>
        /// Entity에 부착되었을 때 호출됩니다.
        /// </summary>
        protected virtual void OnAttachedToEntity(BaseEntity entity) { }

        /// <summary>
        /// Entity에서 분리되었을 때 호출됩니다.
        /// </summary>
        protected virtual void OnDetachedFromEntity() { }
    }
}