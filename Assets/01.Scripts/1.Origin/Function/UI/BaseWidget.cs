using System.Collections;
using System.Collections.Generic;
using Akasha;
using UnityEngine;

public abstract class BaseWidget : BaseUI, IFunctionalSubscriber, IInteractLogicalSubscriber
{
    public virtual void Refresh() { }
    protected virtual void OnSetup() { }
    protected virtual void Awake() { OnSetup(); }
}