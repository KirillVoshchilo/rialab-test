using App.SimplesScipts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.AppInputSystem
{
    public sealed class WorldInput : MainInputActions.IWorldActions, IWorldInput
    {
        private readonly MainInputActions _interactions;
        private readonly SEvent<bool> _onMoving = new();
        private readonly SEvent<bool> _onLooking = new();
        private readonly SEvent _onInteractionPressed = new();
        private readonly SEvent _onJumpPressed = new();
        private Vector2 _moveDirection;
        private Vector2 _lookDirection;
        private bool _isMoving;
        private bool _isLooking;
        private bool _isEnable;

        public SEvent<bool> OnMoving => _onMoving;
        public SEvent OnJumpPressed => _onJumpPressed;
        public SEvent<bool> OnLooking => _onLooking;
        public Vector2 MoveDirection => _moveDirection;
        public Vector2 LookDirection => _lookDirection;
        public bool IsMoving => _isMoving;
        public bool IsLooking => _isLooking;
        public bool IsEnable
        {
            get => _isEnable;
            set
            {
                if (value == _isEnable)
                    return;

                Debug.Log($"World {value}");

                _isEnable = value;

                if (value)
                    _interactions.World.Enable();
                else _interactions.World.Disable();
            }
        }
        public SEvent OnInteractionPressed => _onInteractionPressed;

        public WorldInput()
        {
            _interactions = new MainInputActions();
            _interactions.World.SetCallbacks(this);
        }

        #region Input Reactions
        public void OnMove(InputAction.CallbackContext context)
        {
            if (!_isEnable)
                return;

            _moveDirection = context.ReadValue<Vector2>();

            if (_moveDirection != Vector2.zero && !_isMoving)
            {
                _isMoving = true;
                _onMoving.Invoke(true);
            }

            if (_isMoving && _moveDirection == Vector2.zero)
            {
                _isMoving = false;
                _onMoving.Invoke(false);
            }
        }
        public void OnInteract(InputAction.CallbackContext context)
        {
            if (!_isEnable)
                return;

            if (context.phase == InputActionPhase.Performed)
                _onInteractionPressed.Invoke();
        }
        public void OnLook(InputAction.CallbackContext context)
        {
            _lookDirection = context.ReadValue<Vector2>();
            if (_lookDirection != Vector2.zero && !_isLooking)
            {
                _isLooking = true;
                _onLooking.Invoke(true);
            }
            if (_isLooking && _lookDirection == Vector2.zero)
            {
                _isLooking = false;
                _onLooking.Invoke(false);
            }
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (!_isEnable)
                return;

            if (context.phase == InputActionPhase.Performed)
                _onJumpPressed.Invoke();
        }

        #endregion
    }

    public interface IWorldInput
    {
        Vector2 MoveDirection { get; }
        Vector2 LookDirection { get; }
        bool IsMoving { get; }
        bool IsLooking { get; }
        bool IsEnable { get; set; }
        SEvent<bool> OnMoving { get; }
        SEvent OnJumpPressed { get; }
        SEvent<bool> OnLooking { get; }
        SEvent OnInteractionPressed { get; }
    }
}