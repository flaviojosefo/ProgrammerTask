using UnityEngine;

public abstract class ItemBase : MonoBehaviour, IItem
{
    [Header("Main Settings")]
    [SerializeField] private Sprite icon;

    public Sprite Icon => icon;

    public abstract void PickUp();
}
