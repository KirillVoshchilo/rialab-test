using App.AppInputSystem;
using App.Puzzles.PasswordPuzzle.Private;
using App.SimplesScipts;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
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
            _data.CinemachineBrain = Camera.main.transform.GetComponent<CinemachineBrain>();

            ResetValues();
        }
        public T Get<T>() where T : class
        {
            if (typeof(T) == typeof(InteractionObject))
                return _data.InteractionObject as T;

            return null;
        }

        [Button("Reset", ButtonSizes.Large), GUIColor(0.4f, 0.8f, 1)]
        private void ResetValues()
        {
            if (_data.PuzzleWins == null)
                return;

            int password = Random.Range(0, 9999);
            _data.Password = password.ToString(_data.PasswordForm);
            _data.PasswordField.text = _data.Password;
            _data.PasswordInputField.text = "";

            if (_data.IsWinned)
            {
                _data.IsWinned = false;
                _data.PuzzleWins.WinsCount -= 1;
            }
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

            _data.IsWinned = true;
            _data.PuzzleWins.WinsCount += 1;
            FinishPuzzle();
        }
        [Button("FinishPuzzle", ButtonSizes.Large), GUIColor(0.8f, 0.3f, 0.3f)]
        private async void FinishPuzzle()
        {
            if (_data.PuzzleInput == null)
                return;

            _data.VirtualCamera.Priority = 5;
            _data.PuzzleInput.IsEnable = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _data.EnterButtonEvent.RemoveListener(OnEnterButtonClicked);
            _data.ResetButtonEvent.RemoveListener(OnResetButtonClicked);

            int count = _data.NumberButtons.Length;

            for (int i = 0; i < count; i++)
                _data.NumberButtons[i].OnClicked.RemoveListener(OnButtonClicked);


            await UniTask.WaitUntil(() => _data.CinemachineBrain.IsBlending == false);

            _data.WorldInput.IsEnable = true;
        }
    }
}
