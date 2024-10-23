using PuzzleSystem.Core.Data;
using UnityEngine;

namespace PuzzleSystem.Sample
{
    [CreateAssetMenu(menuName = "PuzzleSystem/Sample/PuzzleData")]
    public class SamplePuzzleData : PuzzleData
    {
        [field: SerializeField]
        public KeyCode KeyCode { get; private set; }
        public override void Begin()
        {

        }

        public override bool Refresh()
        {
            return Input.GetKey(KeyCode);
        }

        public override void End(bool isSuccess)
        {

        }
    }
}