using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Input;


public class Player_Manager : MonoBehaviour
{
    //PlayerMov
    [Header("Movement")] 
    public float moveSpeed;

    public float groundDrag;
    [Header("Ground Check")] 
    public float playerHeight;

    public LayerMask whatIsGround;
    private bool grounded;

    public Transform orientation;
    private Vector3 moveDirection;
    //MoveCam
    public Transform cameraPosition;
    public Transform CameraTransform;
    //PlayerCam
    public float sensX;
    public float sensY;

    
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
