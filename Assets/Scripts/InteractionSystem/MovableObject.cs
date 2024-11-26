using UnityEngine;

namespace PhotoSystem
{
    public class MovableObject : MonoBehaviour, IMovable
    {
        private float moveSpeed = 50f;
        private Rigidbody rb;
        
        public void Move(Transform playerPosition, float direction)
        {
            //object position to player position
            Vector3 objectToPlayer = playerPosition.position - transform.position;
            Vector3 moveDirection = Vector3.zero;
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
            
            // Move the object
            rb.AddForce(moveDirection* moveSpeed);
        }
    }
}