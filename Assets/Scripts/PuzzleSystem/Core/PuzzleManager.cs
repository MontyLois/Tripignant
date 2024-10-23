using System;
using System.Collections.Generic;
using LTX.Singletons;
using LTX.Tools;
using PuzzleSystem.Core.Data;

namespace PuzzleSystem.Core.PuzzleSystem.Core
{
    public class PuzzleManager : MonoSingleton<PuzzleManager>
    {
        public event Action<Puzzle> OnPuzzleStarted;
        public event Action<Puzzle> OnPuzzleStopped;

        private List<Puzzle> puzzles;
        private DynamicBuffer<Puzzle> puzzlesBuffer;

        protected override void Awake()
        {
            base.Awake();

            puzzles = new List<Puzzle>();
            puzzlesBuffer = new DynamicBuffer<Puzzle>(64);
        }

        private void Update()
        {
            puzzlesBuffer.CopyFrom(puzzles);

            for (int i = 0; i < puzzlesBuffer.Length; i++)
            {
                //If done, then stop the puzzle
                if(puzzlesBuffer[i].Refresh())
                    StopPuzzle(puzzlesBuffer[i], true);
            }
        }

        public void StartPuzzle(Puzzle puzzle)
        {
            if(puzzles.Contains(puzzle))
                return;

            puzzle.puzzleData.Begin();

            puzzles.Add(puzzle);
            OnPuzzleStarted?.Invoke(puzzle);
        }

        public void StopPuzzle(Puzzle puzzle, bool isSuccess = false)
        {
            puzzle.puzzleData.End(isSuccess);

            if(puzzles.Remove(puzzle))
                OnPuzzleStopped?.Invoke(puzzle);
        }

    }

    
}