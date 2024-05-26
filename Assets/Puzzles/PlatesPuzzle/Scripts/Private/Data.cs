using App.AppInputSystem;
using Cinemachine;
using System;
using UnityEngine;

namespace App.Puzzles.PlatesPuzzle.Private
{
    [Serializable]
    public class Data
    {
        [SerializeField] private GameObject[] _circles;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private Collider _shutdownÑollider;

        private InteractionObject _interactionObject;
        private GameObject _grabbedCircle;
        private IWorldInput _worldInput;
        private PuzzlesWins _puzzlesWins;
        private IPuzzleInput _puzzleInput;
        private Camera _mainCamera;
        private CinemachineBrain _cinemachineBrain;
        private bool _isWinned;

        public bool IsWinned { get => _isWinned; set => _isWinned = value; }

        public InteractionObject InteractionObject { get => _interactionObject; set => _interactionObject = value; }
        public CinemachineVirtualCamera VirtualCamera { get => _virtualCamera; set => _virtualCamera = value; }
        public IWorldInput WorldInput { get => _worldInput; set => _worldInput = value; }
        public IPuzzleInput PuzzleInput { get => _puzzleInput; set => _puzzleInput = value; }
        public PuzzlesWins PuzzleWins { get => _puzzlesWins; set => _puzzlesWins = value; }
        public Camera MainCamera { get => _mainCamera; set => _mainCamera = value; }
        public GameObject GrabbedCircle { get => _grabbedCircle; set => _grabbedCircle = value; }
        public GameObject[] Circles => _circles;
        public Collider ShutdownÑollider => _shutdownÑollider;
        public CinemachineBrain CinemachineBrain { get => _cinemachineBrain; set => _cinemachineBrain = value; }
    }
}