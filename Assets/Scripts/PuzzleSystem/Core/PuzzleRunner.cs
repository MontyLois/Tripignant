using PuzzleSystem.Core.Interfaces;

namespace PuzzleSystem.Core
{
    public sealed class PuzzleRunner<T> : IPuzzleRunner
        where T : IPuzzleContext
    {
        public IPuzzle Puzzle => puzzle;

        private readonly Puzzle<T> puzzle;
        private readonly IPuzzleHandler<T> handler;

        public PuzzleRunner(Puzzle<T> puzzle, IPuzzleHandler<T> handler)
        {
            this.puzzle = puzzle;
            this.handler = handler;
        }

        public void Begin()
        {
            T context = handler.GetContext();

            puzzle.Begin(ref context);
        }

        public bool Refresh()
        {
            T context = handler.GetContext();
            return puzzle.Refresh(ref context);
        }

        public void End(bool isSuccess)
        {
            T context = handler.GetContext();

            puzzle.End(ref context, isSuccess);
        }
    }
}