using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("Camera")]
    [SerializeField] private float cameraSensitivity = 0.5f;
    [SerializeField] private float minPitchAngle = -60f;
    [SerializeField] private float maxPitchAngle = 60f;
    [SerializeField] private Transform cameraTarget;

    private InputManager _input;
    private CharacterController _controller;

    private Vector2 _cameraRotation;

    private void Start()
    {
        _input = GetComponent<InputManager>();
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void LateUpdate()
    {
        RotateCamera();
    }

    private void Move()
    {
        Vector3 moveDirection = new(_input.Move.x, 0f, _input.Move.y);

        _controller.Move(moveSpeed * Time.deltaTime * moveDirection);
    }

    private void RotateCamera()
    {
        if (_input.Look.sqrMagnitude > float.Epsilon)
        {
            _cameraRotation += _input.Look * cameraSensitivity;
        }

        // Yaw
        _cameraRotation.x = Mathf.Clamp(_cameraRotation.x, float.MinValue, float.MaxValue);

        // Pitch
        _cameraRotation.y = Mathf.Clamp(_cameraRotation.y, minPitchAngle, maxPitchAngle);

        cameraTarget.rotation = Quaternion.Euler(-_cameraRotation.y, _cameraRotation.x, 0f);
    }
}
