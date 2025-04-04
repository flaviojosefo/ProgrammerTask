using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("Animations")]
    [SerializeField] private float animSmoothDelta = 15f; // Smooths setting of animator's variables

    [Header("Camera")]
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float cameraSensitivity = 0.5f;
    [SerializeField] private float minPitchAngle = -60f;
    [SerializeField] private float maxPitchAngle = 60f;
    [SerializeField] private Transform cameraTarget;

    private Animator _animator;
    private InputManager _input;
    private CharacterController _controller;

    private Vector2 _cameraRotation;

    private int _animForwardID;
    private int _animRightID;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _input = GetComponent<InputManager>();
        _controller = GetComponent<CharacterController>();

        FetchAnimationIDs();
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
        // Get the camera's directional vectors
        Vector3 forward = mainCamera.forward;
        Vector3 right = mainCamera.right;

        // Project said vectors into 2D in the XZ plane
        forward.y = right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Obtain normalized player input
        Vector3 inputDirection = new Vector3(_input.Move.x, 0f, _input.Move.y).normalized;

        Vector3 targetDirection = Vector3.zero;

        // Check if there's mmovement input
        if (_input.Move != Vector2.zero)
        {
            targetDirection = (forward * inputDirection.z) + (right * inputDirection.x);

            Vector3 targetRotation = forward * (Mathf.Abs(inputDirection.z) + Mathf.Abs(inputDirection.x));

            // Rotate the player
            transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(targetRotation), 0.9f);
        }

        // Add small "gravity" force
        targetDirection += Vector3.down;

        // Move the player
        _controller.Move(moveSpeed * Time.deltaTime * targetDirection.normalized);

        // Fetch current animator values
        float forwardAnim = _animator.GetFloat(_animForwardID);
        float rightAnim = _animator.GetFloat(_animRightID);

        // Set new (smoothed) animator values
        _animator.SetFloat(_animForwardID, Mathf.Lerp(forwardAnim, _input.Move.y, Time.deltaTime * animSmoothDelta));
        _animator.SetFloat(_animRightID, Mathf.Lerp(rightAnim, _input.Move.x, Time.deltaTime * animSmoothDelta));
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

    private void FetchAnimationIDs()
    {
        _animForwardID = Animator.StringToHash("Forward");
        _animRightID = Animator.StringToHash("Right");
    }

    private void OnFootstep(AnimationEvent animEvent)
    {
        // Play sound
    }
}
