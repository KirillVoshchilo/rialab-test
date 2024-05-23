using App.Runtime.Architecture.AppInputSystem;

namespace App.Runtime.Content.Player.Private
{
    public sealed class JumpHandler
    {
        private readonly PlayerData _data;
        private bool _isEnable;

        public bool IsEnable
        {
            get => _isEnable;
            set
            {
                if (value == _isEnable)
                    return;

                _isEnable = value;
                IAppInput appInput = _data.AppInput;

                if (value)
                {
                    appInput.OnJumpPressed.AddListener(OnJump);
                }
                else
                {
                    appInput.OnJumpPressed.RemoveListener(OnJump);
                }
            }
        }

        public JumpHandler(PlayerData data)
        {
            _data = data;
        }

        private void OnJump()
        {
            if (_isEnable
                && _data.IsGrounded)
            {
                _data.VerticalSpeed = _data.JumpSpeed;
            }
        }
    }
}