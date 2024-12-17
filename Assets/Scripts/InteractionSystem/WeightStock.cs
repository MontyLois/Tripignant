using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InterractionSystem.Game
{
    public class WeightStock : MonoBehaviour, IStackable
    {
        public void AddWeight(GameObject weight)
        {
            //set new parent
            weight.transform.SetParent(this.transform);
            
            //reset position and rotation
            weight.transform.localPosition = Vector3.zero;
            weight.transform.localRotation = Quaternion.identity;
            
            //stack the child next to the others.
            float gap = 0;
            Collider collider = weight.GetComponent<Collider>();
            if (collider)
            {
                collider.enabled = true;
                gap = collider.bounds.size.x;
            }
            float nbchild = this.transform.childCount;
            float position_x = gap * (nbchild-1)+ (float)0.05 *(nbchild-1);
            Vector3 position = new Vector3(position_x,0,0);
            weight.transform.localPosition = position;
            
        }

        public GameObject RemoveWeight()
        {
            int child_number = this.transform.childCount -1;
            if (child_number < 0)
            {
                return null;
            }
            GameObject weight = this.transform.GetChild(child_number).gameObject;
            return weight;
        }
    }
}
