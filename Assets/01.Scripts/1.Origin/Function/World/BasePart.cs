using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akasha
{
    /// <summary>
    /// Part는 Entity에 속할 수 있는 독립 기능 단위입니다.
    /// </summary>
    public abstract class BasePart : MonoBehaviour, IFunctionalSubscriber
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

        /// <summary>
        /// 초기화 시 호출됩니다.
        /// </summary>
        public virtual void OnInitialize() { }

        /// <summary>
        /// 종료 시 호출됩니다.
        /// </summary>
        public virtual void OnTerminate() { }

        /// <summary>
        /// Entity에 연결되었을 때 호출됩니다.
        /// </summary>
        protected virtual void OnAttachedToEntity(BaseEntity entity) { }

        /// <summary>
        /// Entity에서 분리되었을 때 호출됩니다.
        /// </summary>
        protected virtual void OnDetachedFromEntity() { }
    }
}


