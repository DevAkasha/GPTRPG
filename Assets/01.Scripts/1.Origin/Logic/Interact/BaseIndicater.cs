using UnityEngine;

namespace Akasha
{
    /// <summary>
    /// Actor의 자식 GameObject에 존재하며,
    /// 사운드, 이펙트, UI 요소 등을 담당하는 독립적인 로직 단위.
    /// Reactive 흐름을 구독할 수 있으며, Actor의 표현 책임을 분산시킴.
    /// </summary>
    public abstract class BaseIndicator : MonoBehaviour, IInteractLogicalSubscriber, IIndicator
    {
        [Tooltip("Indicator의 루트 GameObject (비활성화 대상)")]
        [SerializeField] private GameObject? root;

        /// <summary>
        /// Indicator의 비활성화 대상이 되는 루트 오브젝트
        /// </summary>
        public GameObject Root => root ??= gameObject;

        protected virtual void Awake()
        {
            if (root == null)
                root = gameObject;

            OnSetup();
        }

        protected virtual void OnEnable() => OnActivate();
        protected virtual void OnDisable() => OnDeactivate();

        /// <summary>
        /// 초기 설정 (컴포넌트 캐싱 등).
        /// </summary>
        protected virtual void OnSetup() { }

        /// <summary>
        /// Reactive 구독 등록 등 활성화 시 처리.
        /// </summary>
        protected abstract void OnActivate();

        /// <summary>
        /// 구독 해제 및 리소스 정리.
        /// </summary>
        protected abstract void OnDeactivate();

        /// <summary>
        /// 외부에서 호출 가능한 갱신 함수 (선택 구현).
        /// </summary>
        public virtual void Refresh() { }
    }
}