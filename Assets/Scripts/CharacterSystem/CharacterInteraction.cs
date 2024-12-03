using System;
using PhotoSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterSystem.Game.CharacterSystem
{
    public class CharacterInteraction : MonoBehaviour
    {
        // Reference to the item in the player's hand
        private IUsable itemInHand;
        private IMovable currentMovableObject;
        
        [SerializeField]
        private Camera playerCamera;
        [SerializeField]
        private float interactionRange;
        [SerializeField]
        private Transform itemHolder;
        
    
        public void OnUseItem(InputAction.CallbackContext context)
        {
            itemInHand?.Use();
            if (itemInHand != null)
            {
                Debug.Log("interacting");
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            
            if (context.started)
            {
                Ray ray = new Ray
                {
                    origin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)),
                    direction = playerCamera.transform.forward
                };

                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, interactionRange))
                {
                    // Check if the object hit has a MovableObject script
                    IMovable movable = hitInfo.collider.GetComponent<MovableObject>();
                    if (movable != null)
                    {
                        Debug.Log("interacting");
                        currentMovableObject = movable;
                    }
                    IUsable usable = hitInfo.collider.GetComponent<IUsable>();
                    if (usable != null)
                    {
                        Debug.Log("pickingupitem");
                        if (itemInHand == null)
                        {
                            hitInfo.transform.SetParent(itemHolder.transform);
                            hitInfo.transform.localPosition= Vector3.zero;
                            hitInfo.transform.localRotation=Quaternion.identity;
                        }
                        itemInHand = usable;
                    }
                }
            }
            else if (context.canceled)
            {
                currentMovableObject = null;
            }
            
        }

        public void OnMove(InputAction.CallbackContext context)
        {
                if (currentMovableObject != null)
                {
                    currentMovableObject.Move(transform,context.ReadValue<Vector2>().y);
                }
        }
        
    }
}

