using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    private float moveSpeed = 3f;

    private InputManager _input;
    private CharacterController _controller;

    private void Start()
    {
        _input = GetComponent<InputManager>();
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveDirection = new(_input.Move.x, 0f, _input.Move.y);

        _controller.Move(moveSpeed * Time.deltaTime * moveDirection);
    }
}
