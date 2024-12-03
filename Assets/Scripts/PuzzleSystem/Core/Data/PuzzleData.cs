using UnityEngine;

namespace PuzzleSystem.Core.Data
{
    public abstract class PuzzleData : ScriptableObject
    {
        [field: SerializeField]
        public string PuzzleName { get; private set; }
        
    }
}