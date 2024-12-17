using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InterractionSystem.Game
{
    public class Pulley : MonoBehaviour, IStackable
    { 
        [SerializeField]
        private float height_max;
        private float height;
        [SerializeField]
        private int nb_weight_max;
        private int nb_weight=0;
        [SerializeField] 
        private Transform stageSet;
        
       public void AddWeight(GameObject weight)
       {
           //set new parent
           weight.transform.SetParent(this.transform);
           
           //reset position and rotation
           weight.transform.localPosition = Vector3.zero;
           weight.transform.localRotation = Quaternion.identity;
           
           //stack the child on top of the others.
           float gap = 0;
           Collider collider = weight.GetComponent<Collider>();
           if (collider)
           {
               collider.enabled = true;
               gap = collider.bounds.size.y;
           }
           float nbchild = this.transform.childCount;
           float position_y = gap * (nbchild-1)+(float)0.05 *(nbchild-1);
           Vector3 position = new Vector3(0,position_y,0);
           weight.transform.localPosition = position;
           
           //add weight to the pulley and move the stage set
           nb_weight++;
           ChangeHeight();
       }

       public GameObject RemoveWeight()
       {
           nb_weight--;
           ChangeHeight();
           //ensure LIFO 
           int child_number = this.transform.childCount -1;
           if (child_number < 0)
           {
               return null;
           }
           GameObject weight = this.transform.GetChild(child_number).gameObject;
           return weight;
       }

       private void ChangeHeight()
       {
           //calculate how much the number of weight will lift up the stage set
           float ratio = (float) nb_weight / nb_weight_max;
           //cap to 1
           if (ratio > 1)
           {
               ratio = 1;
           }
           //calculate the height
           height = ratio * height_max;
           //set new height
           if (stageSet)
           {
               
               Vector3 position = new Vector3(stageSet.localPosition.x, height, stageSet.localPosition.z);
               stageSet.localPosition = position;
           }
       }
    }
}
