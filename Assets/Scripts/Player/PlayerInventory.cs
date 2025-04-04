using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private Transform inventoryMenu;
    [SerializeField] private InputManager input;
    [SerializeField] private PlayerController playerController;

    [Header("Slots Settings")]
    [SerializeField] private Transform[] slots;

    private bool _inventoryOpen = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        OpenInventory();
    }

    private void OpenInventory()
    {
        if (input.OpenInventory)
        {
            playerController.enabled = _inventoryOpen;
            inventoryMenu.gameObject.SetActive(!_inventoryOpen);
            ShowCursor(!_inventoryOpen);

            _inventoryOpen = !_inventoryOpen;

            input.OpenInventory = false;
        }
    }

    public bool TryAddItem(string name, Sprite sprite)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // Fetch the slot's icon & tooltip
            GameObject icon = slots[i].GetChild(0).gameObject;
            Transform tooltip = slots[i].GetChild(1);

            if (!icon.activeSelf)
            {
                icon.GetComponent<Image>().sprite = sprite;
                icon.SetActive(true);

                tooltip.GetChild(0).GetComponent<TMP_Text>().text = name;

                return true;
            }
        }

        return false;
    }

    public void RemoveItem()
    {

    }

    public void MoveItem()
    {

    }

    public void UseItem()
    {

    }

    private void ShowCursor(bool value)
    {
        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            ItemBase item = other.GetComponent<ItemBase>();

            if (TryAddItem(item.Name, item.Icon))
            {
                other.GetComponent<ItemBase>().PickUp();
            }
        }
    }
}
