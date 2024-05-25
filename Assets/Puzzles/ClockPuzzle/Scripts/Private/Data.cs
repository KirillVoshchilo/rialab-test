using App.AppInputSystem;
using Cinemachine;
using System;
using TMPro;
using UnityEngine;

namespace App.Puzzles.ClockPuzzle.Private
{
    [Serializable]
    public sealed class Data
    {
        [SerializeField] private GameObject _shortArrow;
        [SerializeField] private GameObject _longArrow;
        [SerializeField] private TextMeshProUGUI _timerField;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private Transform _rotationCenter;

        private int _hours;
        private int _minutes;

        private InteractionObject _interactionObject;
        private GameObject _grabbedArrow;
        private IWorldInput _worldInput;
        private PuzzlesWins _puzzlesWins;
        private IPuzzleInput _puzzleInput;
        private bool _isWinned;
        private Camera _mainCamera;

        public GameObject ShortArrow => _shortArrow;
        public GameObject LongArrow => _longArrow;
        public TextMeshProUGUI TimerField => _timerField;

        public InteractionObject InteractionObject { get => _interactionObject; set => _interactionObject = value; }
        public CinemachineVirtualCamera VirtualCamera { get => _virtualCamera; set => _virtualCamera = value; }
        public IWorldInput WorldInput { get => _worldInput; set => _worldInput = value; }
        public IPuzzleInput PuzzleInput { get => _puzzleInput; set => _puzzleInput = value; }
        public PuzzlesWins PuzzleWins { get => _puzzlesWins; set => _puzzlesWins = value; }
        public bool IsWinned { get => _isWinned; set => _isWinned = value; }
        public Camera MainCamera { get => _mainCamera; set => _mainCamera = value; }
        public GameObject GrabbedArrow { get => _grabbedArrow; set => _grabbedArrow = value; }
        public Transform RotationCenter => _rotationCenter;
        public int Hours { get => _hours; set => _hours = value; }
        public int Minutes { get => _minutes; set => _minutes = value; }
    }
}