using UnityEngine;

public abstract class InteractableData : BaseData
{
    [Header("공통 프로퍼티")]
    public string itemName;
    public string description;
    public InteractionType interactionType;
    public InteractalbeCategory interactionCategory;
    public bool isDisposable; 
    public float destroyDelay;
}
