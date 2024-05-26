using App.Puzzles;
using System;
using UnityEngine;

namespace App.CentralMechanism.Private
{
    [Serializable]
    public sealed class Data
    {
        [SerializeField] private GameObject[] _details;
        [SerializeField] private Transform[] _path;

        private PuzzlesWins _puzzlesWins;
        private bool[] _isDetailActive;
        private Vector3 _defaultScale;

        public PuzzlesWins PuzzleWins { get => _puzzlesWins; set => _puzzlesWins = value; }
        public GameObject[] Details => _details;

        public Transform[] Path => _path;

        public bool[] IsDetailActive { get => _isDetailActive; set => _isDetailActive = value; }
        public Vector3 DefaultScale { get => _defaultScale; set => _defaultScale = value; }
    }
}