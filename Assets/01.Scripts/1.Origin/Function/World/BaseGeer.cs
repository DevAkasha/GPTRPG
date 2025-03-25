using System.Collections;
using System.Collections.Generic;
using Akasha;
using UnityEngine;
public abstract class BaseGeer : BasePart, IInteractLogicalSubscriber
{
    protected virtual void SetupInteractions() { }
    protected virtual void TeardownInteractions() { }

    protected override void OnAttachedToEntity(BaseEntity entity)
    {
        base.OnAttachedToEntity(entity);
        SetupInteractions();
    }

    protected override void OnDetachedFromEntity()
    {
        TeardownInteractions();
        base.OnDetachedFromEntity();
    }
}
