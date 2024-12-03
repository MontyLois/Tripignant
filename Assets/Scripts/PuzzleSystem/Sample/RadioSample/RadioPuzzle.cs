using PuzzleSystem.Core;
using UnityEngine;

namespace PuzzleSystem.Sample.RadioSample
{
    public class RadioPuzzle : Puzzle<RadioContext>
    {
        private AudioClip audioClip;
        public RadioPuzzle(AudioClip audioClip)
        {
            this.audioClip = audioClip;
        }

        public override void Begin(ref RadioContext context)
        {

        }

        public override bool Refresh(ref RadioContext context)
        {
            return context.radio.HasBattery;
        }

        public override void End(ref RadioContext context, bool isSuccess)
        {
            if(isSuccess)
                context.radio.PlayClip(audioClip);
        }
    }
}