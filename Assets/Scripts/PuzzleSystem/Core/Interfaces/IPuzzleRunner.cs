namespace PuzzleSystem.Core.Interfaces
{
    public interface IPuzzleRunner
    {
        /// <summary>
        /// Puzzle controlled by the runner
        /// </summary>
        IPuzzle Puzzle { get; }


        void Begin();

        bool Refresh();

        void End(bool isSuccess);
    }
}