using UnityEngine;

public class PlayerInteractionManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform mainCamera;
    [SerializeField] private InputManager input;

    [Header("Interaction Settings")]
    [SerializeField] private float detectableDistance = 2f;
    [SerializeField] private InteractableBase[] interactables;

    private void Update()
    {
        DetectInteractable();
    }

    private void DetectInteractable()
    {
        for (int i = 0; i < interactables.Length; i++)
        {
            // Hacky way to force prompt "shutdown"
            interactables[i].HidePrompt();

            Vector3 interactablePos = interactables[i].transform.position;

            // Detect if player is within interactables' range
            if (Vector3.Distance(input.transform.position, interactablePos) < detectableDistance)
            {
                Vector3 camToInteractable = (interactablePos - mainCamera.position).normalized;

                // Detect if player is looking towards the interactable
                if (Vector3.Dot(mainCamera.forward, camToInteractable) > 0.95f)
                {
                    interactables[i].ShowPrompt();

                    // Interact with the object if the player presses the supplied button
                    if (input.Interact)
                    {
                        interactables[i].Interact();

                        input.Interact = false;
                    }
                }
            }
        }
    }
}
