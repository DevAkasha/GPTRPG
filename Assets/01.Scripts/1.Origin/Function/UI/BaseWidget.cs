using System.Collections;
using System.Collections.Generic;
using Akasha;
using UnityEngine;

public abstract class BaseWidget : MonoBehaviour, IFunctionalSubscriber, IInteractLogicalSubscriber
{
    protected virtual void Awake() { OnSetup(); }
    public virtual void Refresh() { }
    protected virtual void OnSetup() { }  
}