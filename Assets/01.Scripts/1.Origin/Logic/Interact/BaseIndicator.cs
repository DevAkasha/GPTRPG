using UnityEngine;

namespace Akasha
{
    /// <summary>
    /// 독립적으로 표현을 수행하는 단위입니다. Actor의 부담을 줄여줄 대체자역할을 합니다.
    /// ReactiveField를 직접 구독하여 이펙트, 사운드 등을 제어합니다.
    /// </summary>
    public abstract class BaseIndicator : MonoBehaviour, IInteractLogicalSubscriber, IIndicator
    {
        [Tooltip("Indicator의 루트 GameObject (비활성화 대상)")]
        [SerializeField] private GameObject? root;

        /// <summary>
        /// Indicator의 루트 GameObject
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
        /// 수동 Refresh 시 수행할 로직 (선택적)
        /// </summary>
        public virtual void Refresh() { }
    }
}