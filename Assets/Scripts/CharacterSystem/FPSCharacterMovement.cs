using System;
using ECM2;
using PhotoSystem;
using UnityEngine;
using UnityEngine.InputSystem;


public class CharacterInput : MonoBehaviour
{
    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow.")]
    [SerializeField]
    private GameObject cameraTarget;
    [Tooltip("How far in degrees can you move the camera up.")]
    private float maxPitch = 80.0f;
    [Tooltip("How far in degrees can you move the camera down.")]
    private float minPitch = -80.0f;
        
    [Space(15.0f)]
    [Tooltip("Cinemachine Virtual Camera positioned at desired crouched height.")]
    [SerializeField]
    private GameObject crouchedCamera;
    [Tooltip("Cinemachine Virtual Camera positioned at desired un-crouched height.")]
    [SerializeField]
    private GameObject unCrouchedCamera;
        
    [Space(15.0f)]
    [Tooltip("Mouse look sensitivity")]
    [SerializeField]
    private Vector2 lookSensitivity = new Vector2(1.5f, 1.25f);

    // Cached controlled character
    private Character _character;
    private PlayerInput _input;
    
    // Current camera target pitch
        
    public float _cameraTargetPitch;
    private InputAction moveAction;

    private bool take_Input;
    
    //for teleportation
    [SerializeField] 
    private Transform destination;


    private void Awake()
    {
        // Cache controlled character.
        _input = GetComponent<PlayerInput>();
        _character = GetComponent<Character>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
            
        // Disable Character's rotation mode, we'll handle it here
            
        _character.SetRotationMode(Character.RotationMode.None);
        moveAction = _input.actions.FindAction("Move");
        take_Input = true;
    }
    private void OnEnable()
    {
        // Subscribe to Character events
            
        _character.Crouched += OnCrouched;
        _character.UnCrouched += OnUnCrouched;
    }
        
    private void OnDisable()
    {
        // Unsubscribe to Character events
            
        _character.Crouched -= OnCrouched;
        _character.UnCrouched -= OnUnCrouched;
    }

    private void Update()
    {
       Move(moveAction.ReadValue<Vector2>()); 
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        ITeleport teleporter = other.gameObject.GetComponent<ITeleport>();
        if (teleporter != null)
        {
            Debug.Log(other.gameObject);
            teleporter.Teleport(_character);
        }
    }
    

    /// <summary>
    /// Add input (affecting Yaw).
    /// This is applied to the Character's rotation.
    /// </summary>
        
    public void AddControlYawInput(float value)
    {
        _character.AddYawInput(value);
    }
        
    /// <summary>
    /// Add input (affecting Pitch).
    /// This is applied to the cameraTarget's local rotation.
    /// </summary>
        
    public void AddControlPitchInput(float value, float minValue = -80.0f, float maxValue = 80.0f)
    {
        if (value == 0.0f)
            return;
            
        _cameraTargetPitch = MathLib.ClampAngle(_cameraTargetPitch + value, minValue, maxValue);
        cameraTarget.transform.localRotation = Quaternion.Euler(-_cameraTargetPitch, 0.0f, 0.0f);
    }
    
    /// <summary>
    /// Movement InputAction event handler.
    /// </summary>

    public void OnMove(InputAction.CallbackContext context)
    {
        // Read input values

        Vector2 inputMovement = context.ReadValue<Vector2>();

        //Move(inputMovement);
        
    }

    private void Move(Vector2 direction)
    {
        // Compose a movement direction vector in world space

        Vector3 movementDirection = Vector3.zero;

        if (take_Input)
        {
            movementDirection += Vector3.forward * direction.y;
            movementDirection += Vector3.right * direction.x;

            // If character has a camera assigned,
            // make movement direction relative to this camera view direction

            if (_character.camera)
            {               
                movementDirection 
                    = movementDirection.relativeTo(_character.cameraTransform);
            }
        }
        
    
        // Set character's movement direction vector
        _character.SetMovementDirection(movementDirection);
    }

    /// <summary>
    /// Jump InputAction event handler.
    /// </summary>

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            _character.Jump();
        else if (context.canceled)
            _character.StopJumping();
    }

    /// <summary>
    /// Crouch InputAction event handler.
    /// </summary>

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
            _character.Crouch();
        else if (context.canceled)
            _character.UnCrouch();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        //read input values
        Vector2 lookInput = context.ReadValue<Vector2>() * Time.deltaTime;
        
        // Add yaw input, this update character's yaw rotation

        AddControlYawInput(lookInput.x * lookSensitivity.x);
            
        // Add pitch input (look up / look down), this update cameraTarget's local rotation
            
        AddControlPitchInput(lookInput.y * lookSensitivity.y, minPitch, maxPitch);
        
    }

    /// <summary>
    /// When character crouches, toggle Crouched / UnCrouched cameras.
    /// </summary>
        
    private void OnCrouched()
    {
        crouchedCamera.SetActive(true);
        unCrouchedCamera.SetActive(false);
    }
        
    /// <summary>
    /// When character un-crouches, toggle Crouched / UnCrouched cameras.
    /// </summary>
        
    private void OnUnCrouched()
    {
        crouchedCamera.SetActive(false);
        unCrouchedCamera.SetActive(true);
    }
    
    public void OnTeleport(InputAction.CallbackContext context)
    {
        _character.SetPosition(destination.position);
        _character.SetRotation(destination.rotation);
    }
    
    public void LockCharacter()
    {
        take_Input = false;
    }

    public void UnlockCharacter()
    {
        take_Input = true;
    }
}
