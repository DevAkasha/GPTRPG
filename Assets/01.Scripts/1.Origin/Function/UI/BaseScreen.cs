using UnityEngine;

namespace Akasha
{
    /// <summary>
    /// 유저 입력이 가능한 UI 화면의 기본 클래스입니다.
    /// Presenter 또는 로직 클래스에 의해 열리고 닫히며, 상태 초기화 및 입력 핸들링을 수행합니다.
    /// </summary>
    public abstract class BaseScreen : MonoBehaviour, IFunctionalSubscriber
    {
        protected virtual void Awake() { OnSetup(); }
        public virtual void Refresh() { }
        protected virtual void OnSetup() { }
    }
}