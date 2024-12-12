using System;
using UnityEngine;

namespace PhotoSystem
{
    public class MovableObject : MonoBehaviour, IMovable
    {
        [SerializeField]
        private float moveSpeed = 50f;
        [SerializeField]
        private Rigidbody rb;
        
        public void Move(Transform playerPosition, float direction)
        {
            Vector3 moveDirection = Vector3.zero;
            
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
            
            //we are calculating the dot product between the player forward and the object forward 
            //to determin on wich axis we should move the object
            float dotproduct = Vector3.Dot(playerPosition.forward, this.transform.forward);
            float absolute = Math.Abs(dotproduct);
            //if it's under 0.5, then the player is align with the z axis of the object
            if (absolute < 0.5)
            {
                moveDirection = this.transform.right;
            }
            //if it's more than 0.5, then the player is align with the x axis of the object
            else
            {
                moveDirection = this.transform.forward;
            }

            //direction : if we are pulling (-1) or pushin (1)
            //dotproduct : neg if we are in front or on the righe, pos if we are behind or on the left.
            moveDirection = moveDirection * direction * dotproduct;
            moveDirection = moveDirection.normalized;
            
            // Move the object
            rb.AddForce(moveDirection * moveSpeed);
        }
    }
}