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

        public PuzzlesWins PuzzleWins { get => _puzzlesWins; set => _puzzlesWins = value; }
        public GameObject[] Details => _details;

        public Transform[] Path => _path;
    }
}