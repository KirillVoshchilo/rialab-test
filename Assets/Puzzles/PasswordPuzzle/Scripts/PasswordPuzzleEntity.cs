using App.AppInputSystem;
using App.Puzzles.PasswordPuzzle.Private;
using App.SimplesScipts;
using UnityEngine;
using VContainer;

namespace App.Puzzles.PasswordPuzzle
{
    public sealed class PasswordPuzzleEntity : MonoBehaviour, IEntity
    {
        [SerializeField] private Data _data;

        [Inject]
        public void Construct(IWorldInput worldInput,
            IPuzzleInput puzzleInput,
            PuzzlesWins puzzlesWins)
        {
            _data.InteractionObject = new InteractionObject(InteractiWithPuzzle);
            _data.WorldInput = worldInput;
            _data.PuzzleInput = puzzleInput;
            _data.PuzzleWins = puzzlesWins;

            int password = Random.Range(0, 9999);
            _data.Password = password.ToString(_data.PasswordForm);
            _data.PasswordField.text = _data.Password;
        }
        public T Get<T>() where T : class
        {
            if (typeof(T) == typeof(InteractionObject))
                return _data.InteractionObject as T;

            return null;
        }

        private void InteractiWithPuzzle()
        {
            if (_data.IsWinned)
                return;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            _data.VirtualCamera.Priority = 20;
            _data.PuzzleInput.IsEnable = true;
            _data.WorldInput.IsEnable = false;

            _data.EnterButtonEvent.AddListener(OnEnterButtonClicked);
            _data.ResetButtonEvent.AddListener(OnResetButtonClicked);

            int count = _data.NumberButtons.Length;
            for (int i = 0; i < count; i++)
            {
                _data.NumberButtons[i].OnClicked.AddListener(OnButtonClicked);
            }

        }
        private void OnButtonClicked(int value)
        {
            string text = _data.PasswordInputField.text;

            if (text.Length >= 4)
                return;

            _data.PasswordInputField.text = $"{text}{value}";
        }
        private void OnResetButtonClicked()
        {
            _data.PasswordInputField.text = "";
        }
        private void OnEnterButtonClicked()
        {
            if (_data.PasswordInputField.text != _data.Password)
                return;

            _data.PuzzleWins.WinsCount += 1;
            FinishPuzzle();
        }
        private void FinishPuzzle()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _data.VirtualCamera.Priority = 5;
            _data.PuzzleInput.IsEnable = false;
            _data.WorldInput.IsEnable = true;
            _data.IsWinned = true;

            _data.EnterButtonEvent.RemoveListener(OnEnterButtonClicked);
            _data.ResetButtonEvent.RemoveListener(OnResetButtonClicked);

            int count = _data.NumberButtons.Length;
            for (int i = 0; i < count; i++)
            {
                _data.NumberButtons[i].OnClicked.RemoveListener(OnButtonClicked);
            }
        }
    }
}