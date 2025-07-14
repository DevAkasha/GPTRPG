using UnityEngine;

namespace Akasha
{

    public abstract class BasePresenter : RxContextBehaviour, IPresenter, IInteractLogicalSubscriber
    {
        protected override void OnInit()
        {
            SetupBindings();
        }

        protected abstract void SetupBindings();

        protected override void OnDispose()
        {
            base.OnDispose();
            TeardownBindings();
        }

        protected virtual void TeardownBindings() { }
    }
}