using System;
using LTX.Singletons;
using PuzzleSystem.Core;
using PuzzleSystem.Core.Interfaces;
using PuzzleSystem.Core.Interfaces;
using UnityEngine;

namespace PuzzleSystem.Sample.RadioSample
{
    public class RadioPuzzleHandler : MonoBehaviour, IPuzzleHandler<RadioContext>
    {
        [SerializeField]
        public Radio radio;
        [SerializeField]
        public Battery battery;
        [SerializeField]
        public PlayerExample player;

        [SerializeField]
        public AudioClip clip;

        private RadioPuzzle puzzle;

        /// <summary>
        /// Creation puzzle
        /// </summary>
        private void Start()
        {
            puzzle = new RadioPuzzle(clip);

            PuzzleManager.Instance.StartPuzzle(puzzle, this);
        }

        public RadioContext GetContext()
        {
            return new RadioContext()
            {
                battery = battery,
                radio = radio,
            };
        }
    }
}