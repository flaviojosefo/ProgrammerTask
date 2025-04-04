using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Mouse Cursor Settings")]
    [SerializeField] private bool cursorVisible = true;
    [SerializeField] private CursorLockMode lockMode = CursorLockMode.Locked;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Interact { get; set; }
    public bool OpenInventory { get; set; }

    private void Start()
    {
        // Set desired initial cursor visibility and state
        Cursor.visible = cursorVisible;
        Cursor.lockState = lockMode;
    }

    public void OnMove(InputValue value)
    {
        Move = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        Look = value.Get<Vector2>();
    }

    public void OnInteract(InputValue value)
    {
        Interact = value.isPressed;
    }

    public void OnInventory(InputValue value)
    {
        OpenInventory = value.isPressed;
    }
}
