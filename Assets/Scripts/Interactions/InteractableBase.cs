using UnityEngine;
using System.Collections;

public abstract class InteractableBase : MonoBehaviour, IInteractable
{
    // Controls how long an interactable remains uninteractable (in seconds)
    [Header("Main Settings")]
    [SerializeField] private float timeToInteractable = 2f;
    [SerializeField] private GameObject prompt;

    protected bool _isInteractable = true;
    public abstract void Interact();

    public void ShowPrompt()
    {
        if (_isInteractable)
            prompt.SetActive(true);
    }

    public void HidePrompt()
    {
        prompt.SetActive(false);
    }

    // Allows an interactable to return to interactable state
    protected virtual IEnumerator Reactivate()
    {
        yield return new WaitForSeconds(timeToInteractable);

        _isInteractable = true;
    }
}
