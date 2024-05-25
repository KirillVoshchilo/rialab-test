using App.AppInputSystem;
using Cinemachine;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Puzzles.PasswordPuzzle.Private
{
    [Serializable]
    public sealed class Data
    {
        [SerializeField] private NumberButton[] _numberButtons;

        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _enterButton;

        [SerializeField] private TextMeshProUGUI _passwordField;
        [SerializeField] private TextMeshProUGUI _passwordInputField;

        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        private string _password;

        private InteractionObject _interactionObject;
        private IWorldInput _worldInput;
        private PuzzlesWins _puzzlesWins;
        private IPuzzleInput _puzzleInput;
        private bool _isWinned;
        private string _passwordForm = "####";

        public InteractionObject InteractionObject { get => _interactionObject; set => _interactionObject = value; }
        public CinemachineVirtualCamera VirtualCamera { get => _virtualCamera; set => _virtualCamera = value; }
        public IWorldInput WorldInput { get => _worldInput; set => _worldInput = value; }
        public IPuzzleInput PuzzleInput { get => _puzzleInput; set => _puzzleInput = value; }
        public PuzzlesWins PuzzleWins { get => _puzzlesWins; set => _puzzlesWins = value; }
        public bool IsWinned { get => _isWinned; set => _isWinned = value; }
        public string Password { get => _password; set => _password = value; }

        public Button.ButtonClickedEvent ResetButtonEvent => _resetButton.onClick;
        public Button.ButtonClickedEvent EnterButtonEvent => _enterButton.onClick;
        public string PasswordForm => _passwordForm;

        public TextMeshProUGUI PasswordField { get => _passwordField; set => _passwordField = value; }
        public TextMeshProUGUI PasswordInputField { get => _passwordInputField; set => _passwordInputField = value; }
        public NumberButton[] NumberButtons { get => _numberButtons; set => _numberButtons = value; }
    }
}