            PuzzleManager.Instance.StartPuzzle(puzzle);
using System;
using System.Collections;
using System.Collections.Generic;
using PuzzleSystem.Core;
using UnityEngine;

namespace PuzzleSystem.Sample
{
    public class PuzzleSampleManager : MonoBehaviour
    {
        [SerializeField]
        private SamplePuzzleData puzzleData;

        private Puzzle puzzle;

        private void OnEnable()
        {
            PuzzleManager.Instance.OnPuzzleStarted += OnPuzzleStarted;
            PuzzleManager.Instance.OnPuzzleStopped += OnPuzzleStopped;
        }
        private void OnDisable()
        {
            PuzzleManager.Instance.OnPuzzleStarted -= OnPuzzleStarted;
            PuzzleManager.Instance.OnPuzzleStopped -= OnPuzzleStopped;
        }

        private void Start()
        {
            puzzle = puzzleData.ToPuzzle();
            PuzzleManager.Instance.StartPuzzle(puzzle);
        }

        private void Update()
        {
            if(Input.GetKey(puzzleData.KeyCode))
                puzzle.SetDirty();
        }

        private void OnPuzzleStarted(Puzzle puzzle)
        {
            Debug.Log($"Puzzle {puzzle.puzzleData.PuzzleName} has started");
        }


        private void OnPuzzleStopped(Puzzle puzzle)
        {
            Debug.Log($"Puzzle {puzzle.puzzleData.PuzzleName} has ended");
        }
    }
}