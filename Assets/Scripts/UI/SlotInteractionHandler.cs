using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotInteractionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private GameObject optionsMenu;

    private GameObject _icon;
    private GameObject _toolTip;

    private void Start()
    {
        _icon = transform.GetChild(0).gameObject;
        _toolTip = transform.GetChild(1).gameObject;
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
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (_icon.activeSelf)
            {
                optionsMenu.SetActive(true);
                optionsMenu.transform.position = eventData.position;

                // Fetch option menu's buttons
                Button useButton = optionsMenu.transform.GetChild(0).GetChild(0).GetComponent<Button>();
                Button removeButton = optionsMenu.transform.GetChild(0).GetChild(1).GetComponent<Button>();

                int index = transform.GetSiblingIndex();

                useButton.onClick.RemoveAllListeners();
                useButton.onClick.AddListener(() => inventory.UseItem(index));

                removeButton.onClick.RemoveAllListeners();
                removeButton.onClick.AddListener(() => inventory.RemoveItem(index));
            }
        }
    }
}
