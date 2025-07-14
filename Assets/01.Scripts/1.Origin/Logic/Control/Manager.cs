using UnityEngine;

namespace Akasha
{
    /// <summary>
    /// Manager는 전역 상태를 판단하고 제어하는 글로벌 싱글톤 컨트롤러입니다.
    /// 모든 RxField와 RxGlobalEvent를 구독/발행할 수 있으며, 씬 전환 간에도 유지됩니다.
    /// </summary>
    public abstract class Manager<T> : RxContextBehaviour, IEventManager, IGlobalLogicalSubscriber, IRxGlobalEventSubscriber
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
        /// 씬 전환 시 유지할지 여부 (기본: true)
        /// </summary>
        protected virtual bool IsPersistent => true;

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = (T)this;

                if (IsPersistent)
                    DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            // RxContext 초기화 전에 수행됨
            OnSetup();
        }

        protected override void OnInit()
        {
            RegisterGlobalEvents();
            SetupGlobalBindings();
            HandleGlobalLogic();
            OnManagerInitialized();
        }

        protected virtual void OnEnable() => OnActivate();
        protected virtual void OnDisable() => OnDeactivate();

        protected override void OnDispose()
        {
            base.OnDispose();
            OnTeardown();
        }

        /// <summary> 최초 생성 시 한 번 호출됨 (Rx 이전) </summary>
        protected virtual void OnSetup() { }

        /// <summary> 초기화 이후 로직 처리 (Rx 이후) </summary>
        protected virtual void OnManagerInitialized() { }

        /// <summary> RxGlobalEvent 등록 </summary>
        protected virtual void RegisterGlobalEvents() { }

        /// <summary> RxField 구독 </summary>
        protected virtual void SetupGlobalBindings() { }

        /// <summary> 이벤트 기반 처리 </summary>
        protected virtual void HandleGlobalLogic() { }

        /// <summary> 수동 리소스 해제 </summary>
        protected virtual void OnTeardown() { }

        /// <summary> 활성화 시 호출 </summary>
        protected abstract void OnActivate();

        /// <summary> 비활성화 시 호출 </summary>
        protected abstract void OnDeactivate();
    }
}