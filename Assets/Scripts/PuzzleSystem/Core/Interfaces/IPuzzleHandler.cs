namespace PuzzleSystem.Core.Interfaces
{
    public interface IPuzzleHandler<T> where T : IPuzzleContext
    {
        T GetContext();
    }
}