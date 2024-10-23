using PuzzleSystem.Core.Data;
using UnityEngine;

namespace PuzzleSystem.Sample.PuzzleSystem.Sample
{
    [CreateAssetMenu(menuName = "PuzzleSystem/Sample/ConditionData")]
    public class SampleConditionData : ConditionData
    {
        public override bool IsComplete()
        {
            return true;
        }
    }
}