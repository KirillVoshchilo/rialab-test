using App.SimplesScipts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.AppInputSystem
{
    public class PuzzleInput : MainInputActions.IPuzzleActions, IPuzzleInput
    {
        private readonly MainInputActions _interactions;
        private readonly SEvent<bool> _onClicked = new();

        private bool _isPressed;

        private bool _isEnable;
        private Vector2 _pointerPosition;

        public bool IsEnable
        {
            get => _isEnable;
            set
            {
                if (value == _isEnable)
                    return;

                _isEnable = value;

                if (value)
                    _interactions.Puzzle.Enable();
                else _interactions.Puzzle.Disable();
            }
        }

        public bool IsPressed => _isPressed;
        public SEvent<bool> OnClicked => _onClicked;
        public Vector2 PointerPosition => _pointerPosition;

        public PuzzleInput()
        {
            _interactions = new MainInputActions();
            _interactions.Puzzle.SetCallbacks(this);
        }

        #region Input Reactions

        public void OnPoint(InputAction.CallbackContext context)
        {
            if (!_isEnable)
                return;

            _pointerPosition = context.ReadValue<Vector2>();
        }
        public void OnClick(InputAction.CallbackContext context)
        {
            if (!_isEnable)
                return;

            if (context.phase == InputActionPhase.Performed)
            {
                _isPressed = !_isPressed;
                _onClicked.Invoke(true);
            }
            if (context.phase == InputActionPhase.Canceled)
            {
                _isPressed = false;
                _onClicked.Invoke(false);
            }
            if (context.phase == InputActionPhase.Disabled)
            {
                _isPressed = false;
                _onClicked.Invoke(false);
            }
        }

        #endregion
    }

    public interface IPuzzleInput
    {
        bool IsPressed { get; }
        SEvent<bool> OnClicked { get; }
        Vector2 PointerPosition { get; }
        bool IsEnable { get; set; }
    }
}