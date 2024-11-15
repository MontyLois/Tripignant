using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSystem.Game
{
    public class PlayerMovement : MonoBehaviour
    {

        [Header("Movement")] 
        public float moveSpeed;

        public float groundDrag;
        [Header("Ground Check")] 
        public float playerHeight;

        public LayerMask whatIsGround;
        private bool grounded;

        public Transform orientation;

        private float horizontalInput;

        private float verticalInput;

        private Vector3 moveDirection;

        private Rigidbody rb;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
        }

        private void Update()
        {
            //ground check
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
            
            MyInput();
            SpeedControl();
            if (grounded)
                rb.drag = groundDrag;
            else
                rb.drag = 0;
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        // Update is called once per frame
        void MyInput()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }

        private void MovePlayer()
        {
            //calculate movement direction
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        private void SpeedControl()
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            //limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }
}
