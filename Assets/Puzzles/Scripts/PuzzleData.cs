using App.SimplesScipts;
using System;
using UnityEngine;

namespace App.Puzzles.ClockPuzzle.Private
{
    [Serializable]
    public class PuzzleData
    {
        private readonly SEvent<bool> _onCompleted = new();

        [SerializeField] private bool _isCompleted;

        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (_isCompleted == value)
                    return;

                _isCompleted = value;
                _onCompleted.Invoke(value);
            }
        }

        public SEvent<bool> OnCompleted => _onCompleted;
    }
}