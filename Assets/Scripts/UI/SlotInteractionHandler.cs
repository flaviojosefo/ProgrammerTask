using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotInteractionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, 
    IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private GameObject optionsMenu;

    private GameObject _icon;
    private GameObject _toolTip;

    private int _index;

    private void Start()
    {
        // Preemptively fetch this object's icon and tooltip
        _icon = transform.GetChild(0).gameObject;
        _toolTip = transform.GetChild(1).gameObject;

        // Fetch this object's (static) index in the Scene
        _index = transform.GetSiblingIndex();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Disable the options menu
        optionsMenu.SetActive(false);

        // The "activeness" of an icon indicates an occupied slot
        if (_icon.activeSelf)
        {
            _toolTip.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Disable the slot's tooltip
        _toolTip.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Detect right clicking on an occupied slot
        if (_icon.activeSelf && eventData.button is PointerEventData.InputButton.Right)
        {
            // Activate the options menu and position by the mouse's location (minus small offset)
            optionsMenu.SetActive(true);
            optionsMenu.transform.position = eventData.position - new Vector2(2f, -2f);

            // Fetch option menu's buttons
            Button useButton = optionsMenu.transform.GetChild(0).GetChild(0).GetComponent<Button>();
            Button removeButton = optionsMenu.transform.GetChild(0).GetChild(1).GetComponent<Button>();

            // Check if the slot contains a battery
            // Hacky, but works
            bool isBattery = _toolTip.transform.GetComponentInChildren<TMP_Text>().text == "Battery";

            // Replace Use and Remove buttons' methods
            useButton.onClick.RemoveAllListeners();
            useButton.onClick.AddListener(() => inventory.UseItem(isBattery));
            useButton.onClick.AddListener(() => inventory.RemoveItem(_index)); // Remove item after using

            removeButton.onClick.RemoveAllListeners();
            removeButton.onClick.AddListener(() => inventory.RemoveItem(_index));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Start dragging operation
        if (_icon.activeSelf)
        {
            inventory.StartedDragMove = true;
            inventory.SwapperIndex = _index;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // End dragging operation
        inventory.StartedDragMove = false;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        // Perform dragging operation
        if (inventory.StartedDragMove)
        {
            inventory.MoveItem(_index);
            inventory.SwapperIndex = _index;
        }
    }
}
