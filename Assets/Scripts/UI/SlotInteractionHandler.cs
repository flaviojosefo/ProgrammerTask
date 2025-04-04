using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        _icon = transform.GetChild(0).gameObject;
        _toolTip = transform.GetChild(1).gameObject;

        _index = transform.GetSiblingIndex();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        optionsMenu.SetActive(false);

        if (_icon.activeSelf)
        {
            _toolTip.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _toolTip.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_icon.activeSelf && eventData.button is PointerEventData.InputButton.Right)
        {
            optionsMenu.SetActive(true);
            optionsMenu.transform.position = eventData.position;

            // Fetch option menu's buttons
            Button useButton = optionsMenu.transform.GetChild(0).GetChild(0).GetComponent<Button>();
            Button removeButton = optionsMenu.transform.GetChild(0).GetChild(1).GetComponent<Button>();

            // Hacky way, but works
            bool isBattery = _toolTip.transform.GetComponentInChildren<TMP_Text>().text == "Battery";

            useButton.onClick.RemoveAllListeners();
            useButton.onClick.AddListener(() => inventory.UseItem(isBattery));
            useButton.onClick.AddListener(() => inventory.RemoveItem(_index)); // Remove item after using

            removeButton.onClick.RemoveAllListeners();
            removeButton.onClick.AddListener(() => inventory.RemoveItem(_index));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_icon.activeSelf)
        {
            inventory.StartedDragMove = true;
            inventory.SwapperIndex = _index;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inventory.StartedDragMove = false;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (inventory.StartedDragMove)
        {
            inventory.MoveItem(_index);
            inventory.SwapperIndex = _index;
        }
    }
}
