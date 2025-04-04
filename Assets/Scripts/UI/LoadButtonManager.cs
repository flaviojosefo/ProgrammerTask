using UnityEngine;
using UnityEngine.UI;

public class LoadButtonManager : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().interactable = SaveManager.Instance.SaveFound();
    }
}
