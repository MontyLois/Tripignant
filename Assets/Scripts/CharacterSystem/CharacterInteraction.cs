using System;
using InterractionSystem.Game;
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
        private GameObject weight;
        
        [SerializeField]
        private Camera playerCamera;
        [SerializeField]
        private float interactionRange;
        [SerializeField]
        private Transform itemHolder;
        [SerializeField]
        private Transform secondItemHolder;

        


        private float movingInput;
        private bool take_input;

        private void Start()
        {
            take_input = true;
        }
        
        private void FixedUpdate()
        {
            if (currentMovableObject != null)
            {
                currentMovableObject.Move(transform,movingInput);
            }
        }

        public void OnUseItem(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                if (take_input)
                {
                    itemInHand?.Use();
                }
                
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            
            if (context.started&&take_input)
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
                    IMovable movable = hitInfo.collider.GetComponent<IMovable>();
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
                            hitInfo.collider.enabled = false;
                            hitInfo.transform.SetParent(itemHolder.transform);
                            hitInfo.transform.localPosition= Vector3.zero;
                            hitInfo.transform.localRotation=Quaternion.identity;
                            
                        }
                        itemInHand = usable;
                    }

                    IStackable stackable = hitInfo.collider.GetComponent<IStackable>();
                    if (stackable!=null)
                    {
                        if (weight != null)
                        {
                            stackable.AddWeight(weight);
                            weight = null;
                        }
                        else
                        {
                            weight = stackable.RemoveWeight();
                            weight.GetComponent<Collider>().enabled = false;
                            weight.transform.SetParent(secondItemHolder);
                            weight.transform.localPosition = Vector3.zero;
                            
                        }
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
                if (currentMovableObject != null&& take_input)
                {
                    movingInput = context.ReadValue<Vector2>().y;
                }
        }
        

        public void LockCharacter()
        {
            take_input = false;
        }

        public void UnlockCharacter()
        {
            take_input = true;
        }

        
    }
}

