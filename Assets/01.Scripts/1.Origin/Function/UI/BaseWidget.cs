using UnityEngine;

namespace Akasha
{
    public abstract class BaseWidget : RxContextBehaviour, IRxUnsafe
    {
        protected override void OnInit()
        {
            RegisterFields();
            BindWidget();
            SetupUIEvents();
            OnWidgetInitialized();
        }

        protected virtual void RegisterFields() { }

        protected virtual void BindWidget() { }

        protected virtual void SetupUIEvents() { }

        protected virtual void OnWidgetInitialized() { }

        public abstract void RefreshUI();

        protected override void OnDispose()
        {
            base.OnDispose();
            OnTeardown();
        }

        protected virtual void OnTeardown() { }
    }
}