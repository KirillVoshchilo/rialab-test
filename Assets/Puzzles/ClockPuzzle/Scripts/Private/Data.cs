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

        [Range(0, 24)]
        [SerializeField] private int _hours;
        [Range(0, 60)]
        [SerializeField] private int _minutes;

        private InteractionObject _interactionObject;
        private GameObject _grabbedArrow;
        private IWorldInput _worldInput;
        private PuzzlesWins _puzzlesWins;
        private IPuzzleInput _puzzleInput;
        private bool _isWinned;
        private Camera _mainCamera;

        public float HoursAngle => _hours * 360 / 24;
        public float MinutesAngle => _minutes * 360 / 60;

        public GameObject ShortArrow => _shortArrow;
        public GameObject LongArrow => _longArrow;
        public TextMeshProUGUI TimerField => _timerField;

        public int Hours => _hours;
        public int Minutes => _minutes;

        public InteractionObject InteractionObject { get => _interactionObject; set => _interactionObject = value; }
        public CinemachineVirtualCamera VirtualCamera { get => _virtualCamera; set => _virtualCamera = value; }
        public IWorldInput WorldInput { get => _worldInput; set => _worldInput = value; }
        public IPuzzleInput PuzzleInput { get => _puzzleInput; set => _puzzleInput = value; }
        public PuzzlesWins PuzzleWins { get => _puzzlesWins; set => _puzzlesWins = value; }
        public bool IsWinned { get => _isWinned; set => _isWinned = value; }
        public Camera MainCamera { get => _mainCamera; set => _mainCamera = value; }
        public GameObject GrabbedArrow { get => _grabbedArrow; set => _grabbedArrow = value; }
        public Transform RotationCenter => _rotationCenter;
    }
}