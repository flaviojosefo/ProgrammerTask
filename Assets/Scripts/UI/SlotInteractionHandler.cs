using UnityEngine;
using UnityEngine.EventSystems;

public class SlotInteractionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private GameObject optionsMenu;

    private GameObject icon;
    private GameObject toolTip;

    private void Start()
    {
        icon = transform.GetChild(0).gameObject;
        toolTip = transform.GetChild(1).gameObject;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        optionsMenu.SetActive(false);

        if (icon.activeSelf)
        {
            toolTip.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (icon.activeSelf)
            {
                optionsMenu.SetActive(true);
                optionsMenu.transform.position = eventData.position;
            }
        }
    }
}
