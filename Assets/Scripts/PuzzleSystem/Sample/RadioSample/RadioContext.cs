using PuzzleSystem.Core.Interfaces;

namespace PuzzleSystem.Sample.RadioSample
{
    public struct RadioContext : IPuzzleContext
    {
        public Radio radio;
        public Battery battery;
    }
}