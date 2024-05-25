using App.AppInputSystem;
using UnityEngine;

namespace App.Player.Private
{
    public sealed class GravityHandler
    {
        private const float DELAY = 0.2f;

        private readonly Data _data;
        private float _timer;
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
                    _data.UnityCycles.AddUpdate(UpdateCycle);
                else
                    _data.UnityCycles.RemoveUpdate(UpdateCycle);
            }
        }

        public GravityHandler(Data data) 
            => _data = data;

        private void UpdateCycle()
        {
            _data.VerticalSpeed -= 10.0f * Time.deltaTime;

            if (_data.VerticalSpeed < -10.0f)
                _data.VerticalSpeed = -10.0f;

            Vector3 verticalMove = new(0, _data.VerticalSpeed * Time.deltaTime, 0);
            CollisionFlags flag = _data.CharacterController.Move(verticalMove);

            if (flag == CollisionFlags.Below)
            {
                _data.VerticalSpeed = 0;
                _data.IsGrounded = true;
                _timer = 0;
            }
            else
            {
                _timer += Time.deltaTime;

                if (_timer > DELAY)
                    _data.IsGrounded = false;
            }
        }
    }
}