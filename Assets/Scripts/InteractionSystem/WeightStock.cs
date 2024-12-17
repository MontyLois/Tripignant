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
            weight.transform.localPosition = Vector3.zero;
            weight.transform.localRotation = Quaternion.identity;
        }

        public GameObject RemoveWeight()
        {
            GameObject weight = this.transform.GetChild(0).gameObject;
            return weight;
        }
    }
}
