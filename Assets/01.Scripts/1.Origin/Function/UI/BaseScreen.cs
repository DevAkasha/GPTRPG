using UnityEngine;

namespace Akasha
{
    /// <summary>
    /// 유저 입력이 가능한 UI 화면의 기본 클래스입니다.
    /// Presenter 또는 로직 클래스에 의해 열리고 닫히며, 상태 초기화 및 입력 핸들링을 수행합니다.
    /// </summary>
    public abstract class BaseScreen : BaseUI, IFunctionalSubscriber
    {
        [SerializeField, Tooltip("화면의 루트 오브젝트 (전체 표시/숨김용)")]
        private GameObject? root;

        public GameObject? Root => root;

        protected virtual void Awake()
        {
            if (root == null)
                root = gameObject;

            OnSetup();
        }

        /// <summary>
        /// 버튼 리스너 등록 등 UI 초기 설정
        /// </summary>
        protected virtual void OnSetup() { }

        /// <summary>
        /// Presenter 또는 시스템에 의해 열릴 때 호출됩니다.
        /// 기본 구현은 root.SetActive(true)입니다.
        /// </summary>
        public override void Opened(params object[] param)
        {
            Show();
        }

        /// <summary>
        /// 즉시 숨김 (애니메이션 생략)
        /// </summary>
        public override void HideDirect()
        {
            Hide();
        }

        /// <summary>
        /// 애니메이션 등으로 화면 표시 처리
        /// </summary>
        public virtual void Show()
        {
            if (root != null)
                root.SetActive(true);
        }

        /// <summary>
        /// 애니메이션 등으로 화면 숨김 처리
        /// </summary>
        public virtual void Hide()
        {
            if (root != null)
                root.SetActive(false);
        }

        /// <summary>
        /// 상태 바인딩 재설정 또는 초기화에 사용됩니다.
        /// </summary>
        public abstract void Refresh();
    }
}