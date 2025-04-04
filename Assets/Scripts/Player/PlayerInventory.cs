using System;
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
    [SerializeField] private GameObject optionsMenu;

    [Header("Incremental UI")]
    [SerializeField] private TMP_Text batteries;
    [SerializeField] private TMP_Text magnets;

    private bool _inventoryOpen = false;

    private int _usedBatteries;
    private int _usedMagnets;

    public bool StartedDragMove { get; set; } = false;
    public int SwapperIndex { get; set; } = -1;

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

            optionsMenu.SetActive(false);

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

    public void RemoveItem(int index)
    {
        // Disable Icon
        GameObject icon = slots[index].GetChild(0).gameObject;
        icon.GetComponent<Image>().sprite = null;
        icon.SetActive(false);

        // Disable Tooltip
        Transform tooltip = slots[index].GetChild(1);
        tooltip.GetChild(0).GetComponent<TMP_Text>().text = "Name";
        tooltip.gameObject.SetActive(false);

        // Disable Options Menu
        optionsMenu.SetActive(false);
    }

    public void MoveItem(int toIndex)
    {
        if (SwapperIndex == toIndex)
            return;

        GameObject fromIcon = slots[SwapperIndex].GetChild(0).gameObject;
        GameObject toIcon = slots[toIndex].GetChild(0).gameObject;

        Transform fromTooltip = slots[SwapperIndex].GetChild(1);
        Transform toTooltip = slots[toIndex].GetChild(1);

        // Moving to empty slot
        if (!toIcon.activeSelf)
        {
            // Switch icon to new slot
            toIcon.GetComponent<Image>().sprite = 
                fromIcon.GetComponent<Image>().sprite;
            toIcon.SetActive(true);

            fromIcon.GetComponent<Image>().sprite = null;
            fromIcon.SetActive(false);

            // Replace tooltip
            toTooltip.GetChild(0).GetComponent<TMP_Text>().text = 
                fromTooltip.GetChild(0).GetComponent<TMP_Text>().text;
            toTooltip.gameObject.SetActive(true);
            fromTooltip.GetChild(0).GetComponent<TMP_Text>().text = "Name";
        }
        // Moving to occupied slot
        else
        {
            // Switch icons
            (fromIcon.GetComponent<Image>().sprite, toIcon.GetComponent<Image>().sprite) = 
                (toIcon.GetComponent<Image>().sprite, fromIcon.GetComponent<Image>().sprite);

            // Switch tooltips
            (fromTooltip.GetChild(0).GetComponent<TMP_Text>().text, toTooltip.GetChild(0).GetComponent<TMP_Text>().text) =
                (toTooltip.GetChild(0).GetComponent<TMP_Text>().text, fromTooltip.GetChild(0).GetComponent<TMP_Text>().text);
        }
    }

    public void UseItem(bool isBattery)
    {
        // Increase displayed value
        if (isBattery)
        {
            _usedBatteries++;
            batteries.text = $"{_usedBatteries}";
        }
        else
        {
            _usedMagnets++;
            magnets.text = $"{_usedMagnets}";
        }
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
