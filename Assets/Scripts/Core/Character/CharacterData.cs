using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Calico.Core
{
    [CreateAssetMenu(fileName = "Character", menuName = "Calico/Character")]
    public class CharacterData : ScriptableObject
    {
        [field: SerializeField]
        public int MouseXSensitivity { get; private set; }
        [field: SerializeField]
        public int MouseYSensitivity { get; private set; }
        
        [field: SerializeField]
        public float playerHeight { get; private set; }
        
        [field: SerializeField]
        public GameObject Avatar { get; private set; }
    }
}
