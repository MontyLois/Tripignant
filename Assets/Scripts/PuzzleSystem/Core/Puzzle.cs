using System.Collections;
using System.Collections.Generic;
using PuzzleSystem.Core.Interfaces;
using UnityEngine;

namespace PuzzleSystem.Core
{
    public abstract class Puzzle<T> : IPuzzle
        where T : IPuzzleContext
    {
        /// <summary>
        /// Begins puzzle
        /// </summary>
        /// <param name="context"></param>
        public abstract void Begin(ref T context);

        /// <summary>
        /// Update from the manager
        /// </summary>
        public abstract bool Refresh(ref T context);

        /// <summary>
        /// Ends puzzle
        /// </summary>
        /// <param name="context"></param>
        /// <param name="isSuccess"></param>
        public abstract void End(ref T context, bool isSuccess);
    }
}