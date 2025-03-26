using UnityEngine;

namespace Akasha
{

    /// <summary>
    /// 유저 입력 없이 상태만 표시하는 출력 전용 UI 구성 요소입니다.
    /// Presenter 또는 Actor에 의해 제어되며, 상태를 바인딩하거나 수동으로 Refresh할 수 있습니다.
    /// </summary>
    public abstract class BaseView : MonoBehaviour, IFunctionalSubscriber
    {
        protected virtual void Awake() { OnSetup(); }
        public virtual void Refresh() { }
        protected virtual void OnSetup() { }
    }
}
