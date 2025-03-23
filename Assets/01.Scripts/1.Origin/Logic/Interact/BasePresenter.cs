using UnityEngine;

namespace Akasha
{
    /// <summary>
    /// Presenter는 UIModel 또는 UI 이벤트를 해석하여
    /// Manager 또는 Spirit에게 의미 있는 이벤트를 전달하는 Interact Logic 주체입니다.
    /// </summary>
    public abstract class BasePresenter : MonoBehaviour, IInteractLogicalSubscriber, IReactiveEventIssuer
    {
        /// <summary>
        /// ReactiveField 또는 UI 이벤트 핸들러 등록.
        /// </summary>
        protected abstract void SetupBindings();

        /// <summary>
        /// 구독 해제 및 상태 초기화.
        /// </summary>
        protected abstract void TeardownBindings();

        protected virtual void OnEnable() => SetupBindings();
        protected virtual void OnDisable() => TeardownBindings();
    }
}