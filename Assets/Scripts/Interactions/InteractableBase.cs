using UnityEngine;
using System.Collections;

public abstract class InteractableBase : MonoBehaviour, IInteractable
{
    // Controls how long an interactable remains uninteractable (in seconds)
    [Header("Main Settings")]
    [SerializeField] private float timeToInteractable = 2f;
    [SerializeField] private GameObject prompt;

    // Indicates when the interactable is available
    public bool IsInteractable { get; protected set; } = true;

    public abstract void Interact();

    // Displays/hides this interactable's prompt
    public void ShowPrompt(bool show)
    {
        prompt.SetActive(show);
    }

    // Allows an interactable to return to interactable state
    protected virtual IEnumerator Reactivate()
    {
        yield return new WaitForSeconds(timeToInteractable);

        IsInteractable = true;
    }
}
