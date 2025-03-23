using UnityEngine;

namespace Akasha
{
    /// <summary>
    /// Manager는 전역 상태를 판단하고 제어하는 게임의 중심 제어자입니다.
    /// 싱글톤 구조를 가지며 씬을 넘어 유지될 수 있습니다.
    /// </summary>
    public abstract class Manager<T> : MonoBehaviour, IControlLogicalSubscriber
        where T : Manager<T>
    {
        private static T? instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        GameObject singleton = new GameObject(typeof(T).Name);
                        instance = singleton.AddComponent<T>();
                    }
                }
                return instance!;
            }
        }

        public static bool IsInstance => instance != null;

        /// <summary>
        /// 씬 전환 시에도 유지할지 여부 (기본값: true)
        /// </summary>
        protected virtual bool isPersistent => true;

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = (T)this;

                if (isPersistent)
                    DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            OnSetup();
        }

        protected virtual void OnEnable() => OnActivate();
        protected virtual void OnDisable() => OnDeactivate();

        /// <summary>
        /// 최초 생성 시 한 번 호출됨
        /// </summary>
        protected virtual void OnSetup() { }

        /// <summary>
        /// 활성화될 때마다 호출됨
        /// </summary>
        protected abstract void OnActivate();

        /// <summary>
        /// 비활성화될 때마다 호출됨
        /// </summary>
        protected abstract void OnDeactivate();
    }
}