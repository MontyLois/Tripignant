using System.Collections;
using System.Collections.Generic;
using PuzzleSystem.Core.Data;
using UnityEngine;

namespace PuzzleSystem.Core.PuzzleSystem.Core
{
    public class Puzzle
    {
        /// <summary>
        /// Has the object changed
        /// </summary>
        public bool IsDirty { get; private set; }

        //Readonly => can only be assigned in constructor
        public readonly PuzzleData puzzleData;

        public Puzzle(PuzzleData puzzleData)
        {
            this.puzzleData = puzzleData;
            IsDirty = true;
        }


        /// <summary>
        /// Update from the manager
        /// </summary>
        public bool Refresh()
        {
            //If nothing has changed, return
            if(!IsDirty)
                return false;

            //Only one refresh
            IsDirty = false;

            if (!puzzleData.Refresh())
                return false;

            //Done
            return true;
        }

        /// <summary>
        /// Signal something has changed
        /// </summary>
        public void SetDirty()
        {
            IsDirty = true;
        }
    }
}