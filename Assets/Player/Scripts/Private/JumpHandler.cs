using App.AppInputSystem;

namespace App.Player.Private
{
    public sealed class JumpHandler
    {
        private readonly Data _data;
        private bool _isEnable;

        public bool IsEnable
        {
            get => _isEnable;
            set
            {
                if (value == _isEnable)
                    return;

                _isEnable = value;
                IWorldInput appInput = _data.AppInput;

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

        public JumpHandler(Data data)
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