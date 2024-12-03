using System;
using System.Collections.Generic;
using LTX.Singletons;
using LTX.Tools;
using PuzzleSystem.Core.Interfaces;
using UnityEngine;

namespace PuzzleSystem.Core
{
    public class PuzzleManager : MonoSingleton<PuzzleManager>
    {
        public event Action<IPuzzleRunner> OnPuzzleStarted;
        public event Action<IPuzzleRunner> OnPuzzleStopped;

        private List<IPuzzleRunner> puzzleRunners;
        private DynamicBuffer<IPuzzleRunner> puzzleRunnersBuffer;

        protected override void Awake()
        {
            base.Awake();

            puzzleRunners = new List<IPuzzleRunner>();
            puzzleRunnersBuffer = new DynamicBuffer<IPuzzleRunner>(64);
        }

        private void Update()
        {
            puzzleRunnersBuffer.CopyFrom(puzzleRunners);

            for (int i = 0; i < puzzleRunnersBuffer.Length; i++)
            {
                //If done, then stop the puzzle
                if(puzzleRunnersBuffer[i].Refresh())
                    StopPuzzle(puzzleRunnersBuffer[i].Puzzle, true);
            }
        }

        public void StartPuzzle<T>(Puzzle<T> puzzle, IPuzzleHandler<T> puzzleHandler) where T : IPuzzleContext
        {
            //On ne recommence pas les puzzles
            if(TryGetPuzzleRunner(puzzle, out IPuzzleRunner runner))
                return;

            PuzzleRunner<T> puzzleRunner = new PuzzleRunner<T>(puzzle, puzzleHandler);

            puzzleRunners.Add(puzzleRunner);
            puzzleRunner.Begin();

            OnPuzzleStarted?.Invoke(puzzleRunner);
        }

        public void StopPuzzle(IPuzzle puzzle, bool isSuccess = false)
        {
            if (TryGetPuzzleRunner(puzzle, out IPuzzleRunner runner))
            {
                runner.End(isSuccess);
                puzzleRunners.Remove(runner);

                OnPuzzleStopped?.Invoke(runner);
            }
        }


        private bool TryGetPuzzleRunner(IPuzzle puzzle, out IPuzzleRunner puzzleRunner)
        {
            foreach (IPuzzleRunner runner in puzzleRunners)
            {
                if (runner.Puzzle == puzzle)
                {
                    //Match trouvé
                    puzzleRunner = runner;
                    return true;
                }
            }

            //On a rien trouvé
            puzzleRunner = null;
            return false;
        }
    }
}