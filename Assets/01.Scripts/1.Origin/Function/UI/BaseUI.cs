using UnityEngine;

namespace Akasha
{
    /// <summary>
    /// 모든 UI 객체의 공통 기반입니다.
    /// 위치 정보와 열기/닫기 제어 기능을 제공합니다.
    /// </summary>
    public abstract class BaseUI : MonoBehaviour
    {
        public eUIPosition uiPosition;
        /// <summary>
        /// UI가 열릴 때 호출됩니다. 파라미터는 화면에 따라 다를 수 있습니다.
        /// </summary>
        public abstract void Opened(params object[] param);

        /// <summary>
        /// UI를 강제로 숨깁니다.
        /// </summary>
        public abstract void HideDirect();
    }
}
