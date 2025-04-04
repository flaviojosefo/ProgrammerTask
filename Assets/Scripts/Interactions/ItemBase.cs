using UnityEngine;

public abstract class ItemBase : MonoBehaviour, IItem
{
    [Header("Main Settings")]
    [SerializeField] private new string name = "";
    [SerializeField] private Sprite icon;

    public string Name => name;
    public Sprite Icon => icon;

    public abstract void PickUp();
}
