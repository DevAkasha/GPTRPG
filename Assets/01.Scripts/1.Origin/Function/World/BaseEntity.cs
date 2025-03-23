using System.Collections.Generic;
using UnityEngine;

namespace Akasha
{
    /// <summary>
    /// Entity는 여러 Part를 포함하는 World 기능 단위입니다.
    /// Composite 구조이며 기능적 구독자 역할을 합니다.
    /// </summary>
    public abstract class BaseEntity : MonoBehaviour, IFunctionalSubscriber
    {
        private readonly List<BasePart> _parts = new();

        public IReadOnlyList<BasePart> Parts => _parts;

        /// <summary>
        /// Part를 Entity에 등록하고 관계를 설정합니다.
        /// </summary>
        public void AddPart(BasePart part)
        {
            if (part == null || _parts.Contains(part)) return;

            _parts.Add(part);
            part.SetParent(this);

            OnPartAdded(part);
        }

        /// <summary>
        /// Part를 Entity에서 제거하고 관계를 해제합니다.
        /// </summary>
        public void RemovePart(BasePart part)
        {
            if (part == null || !_parts.Contains(part)) return;

            _parts.Remove(part);
            part.SetParent(null);

            OnPartRemoved(part);
        }

        /// <summary>
        /// 특정 타입의 Part를 반환합니다.
        /// </summary>
        public T? GetPart<T>() where T : BasePart
        {
            foreach (var part in _parts)
            {
                if (part is T match)
                    return match;
            }
            return null;
        }

        /// <summary>
        /// Entity 및 모든 Part를 초기화합니다.
        /// </summary>
        public virtual void OnInitialize()
        {
            foreach (var part in _parts)
                part.OnInitialize();
        }

        /// <summary>
        /// Entity 및 모든 Part를 종료하고 정리합니다.
        /// </summary>
        public virtual void OnTerminate()
        {
            foreach (var part in _parts)
                part.OnTerminate();
            _parts.Clear();
        }

        /// <summary>
        /// Part가 추가될 때 확장 Hook.
        /// </summary>
        protected virtual void OnPartAdded(BasePart part) { }

        /// <summary>
        /// Part가 제거될 때 확장 Hook.
        /// </summary>
        protected virtual void OnPartRemoved(BasePart part) { }
    }
}