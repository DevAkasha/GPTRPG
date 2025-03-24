using UnityEngine;

namespace Akasha
{

    /// <summary>
    /// 유저 입력 없이 상태만 표시하는 출력 전용 UI 구성 요소입니다.
    /// Presenter 또는 Actor에 의해 제어되며, 상태를 바인딩하거나 수동으로 Refresh할 수 있습니다.
    /// </summary>
    public abstract class BaseView : BaseUI, IFunctionalSubscriber
    {
        [SerializeField, Tooltip("이 View의 루트 오브젝트 (비활성화용)")]
        private GameObject? root;

        public GameObject? Root => root;

        protected virtual void Awake()
        {
            if (root == null)
                root = gameObject;

            OnSetup();
        }

        /// <summary>
        /// View 초기 설정 (애니메이션 초기화, 컴포넌트 캐싱 등)
        /// </summary>
        protected virtual void OnSetup() { }

        /// <summary>
        /// ReactiveField 등의 상태 변화에 따라 View를 갱신할 때 호출됩니다.
        /// </summary>
        public abstract void Refresh();

        /// <summary>
        /// UI가 열릴 때 호출됩니다. 필요 시 override.
        /// </summary>
        public override void Opened(params object[] param)
        {
            if (root != null)
                root.SetActive(true);
        }

        /// <summary>
        /// UI를 즉시 숨깁니다.
        /// </summary>
        public override void HideDirect()
        {
            if (root != null)
                root.SetActive(false);
        }
    }
}
