using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InterractionSystem.Game
{
    public interface IStackable
    {
        void AddWeight(GameObject Weight);
        GameObject RemoveWeight();
    }
}
