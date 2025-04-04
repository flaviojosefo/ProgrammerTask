using UnityEngine;
using UnityEngine.UI;

public class LoadButtonManager : MonoBehaviour
{
    void Start()
    {
        // Activate the load button if a save is found
        GetComponent<Button>().interactable = SaveManager.Instance.SaveFound();
    }
}
