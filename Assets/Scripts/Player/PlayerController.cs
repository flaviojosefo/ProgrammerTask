using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("Camera")]
    [SerializeField] private Transform mainCamera;
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
        Vector3 forward = mainCamera.forward;
        Vector3 right = mainCamera.right;

        forward.y = right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 inputDirection = new Vector3(_input.Move.x, 0f, _input.Move.y).normalized;

        Vector3 targetDirection = Vector3.zero;

        if (_input.Move != Vector2.zero)
        {
            targetDirection = (forward * inputDirection.z) + (right * inputDirection.x);

            Vector3 targetRotation = (forward * Mathf.Abs(inputDirection.z)) + (forward * Mathf.Abs(inputDirection.x));

            transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(targetRotation), 1f);
        }

        _controller.Move(moveSpeed * Time.deltaTime * targetDirection.normalized);
    }

    private void RotateCamera()
    {
        // Check if there's mouse movement
        if (_input.Look.sqrMagnitude > float.Epsilon)
        {
            _cameraRotation += _input.Look * cameraSensitivity;
        }

        // Yaw
        _cameraRotation.x = Mathf.Clamp(_cameraRotation.x, float.MinValue, float.MaxValue);

        // Pitch
        _cameraRotation.y = Mathf.Clamp(_cameraRotation.y, minPitchAngle, maxPitchAngle);

        // Set Camera's rotation
        cameraTarget.rotation = Quaternion.Euler(-_cameraRotation.y, _cameraRotation.x, 0f);
    }
}
