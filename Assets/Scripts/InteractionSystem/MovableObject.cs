using System;
using UnityEngine;

namespace PhotoSystem
{
    public class MovableObject : MonoBehaviour, IMovable
    {
        //[SerializeField]
        private float moveSpeed = 30f;
        private Rigidbody rb;

        private void Start()
        {
            rb = this.GetComponent<Rigidbody>();
        }

        public void Move(Transform playerPosition, float direction)
        {
            
            /*
            //object position to player position
            Vector3 objectToPlayer = playerPosition.position - transform.position;
            //calculate the relative direction to move the object
            Vector3 relativdir = playerPosition.forward * direction;
            
            //check if the player is on the x axis of the object or the z axis
            if (Mathf.Abs(objectToPlayer.x) > Mathf.Abs(objectToPlayer.z))
            {
                moveDirection = new Vector3(relativdir.x, 0, 0).normalized;
            }
            else
            {
                moveDirection = new Vector3(0, 0, relativdir.z).normalized;
            }
            
            */
            
            //let's try something new
            

            Vector3 playerForward = playerPosition.forward;
            
            //calculating dot product of the player and the right and forward vector of the object
            float rightDot = Vector3.Dot(playerForward, this.transform.right);
            float forwardDot = Vector3.Dot(playerForward, this.transform.forward);
            
            //comparing the dot product to determine on which axis of the object the player is aligned
            var isRight = Mathf.Abs(rightDot) > Mathf.Abs(forwardDot);
            Vector3 axis = isRight ? transform.right : transform.forward;

            //direction : if we are pulling (-1) if we are pushing (1)
            //dotproduct : neg if we are in front or on the right, pos if we are behind or on the left.
            Vector3 moveDirection = axis * (direction * Mathf.Sign(isRight ? rightDot : forwardDot));
            //normalized to ensure a constante movespeed.
            moveDirection = moveDirection.normalized;
            /*
            Debug.DrawRay(playerPosition.position, playerPosition.forward * 50, Color.cyan);
            Debug.DrawRay(transform.position, moveDirection.normalized * 100, isRight ? Color.red : Color.blue);
            */
            // Move the object
            rb.AddForce(moveDirection * (moveSpeed * Time.fixedDeltaTime), ForceMode.VelocityChange);
        }
    }
}