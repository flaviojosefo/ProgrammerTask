using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private Transform inventoryMenu;
    [SerializeField] private InputManager input;
    [SerializeField] private PlayerController playerController;

    [Header("Slots Settings")]
    [SerializeField] private int slotsAmount = 40;

    private bool _inventoryOpen = false;

    private void Start()
    {
        GenerateSlots();
    }

    private void Update()
    {
        OpenInventory();
    }

    private void GenerateSlots()
    {
        for (int i = 0; i < slotsAmount; i++)
        {
            Instantiate(slotPrefab, slotsParent);
        }
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

    public void AddItem(string name)
    {
        for (int i = 0; i < slotsAmount; i++)
        {
            // select the first available slot
        }
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
            other.GetComponent<ItemBase>().PickUp();
        }
    }
}
