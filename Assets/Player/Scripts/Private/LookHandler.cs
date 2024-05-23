using UnityEngine;

namespace App.Runtime.Content.Player.Private
{
    public sealed class LookHandler
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

                if (value)
                {
                    _data.AppInput.OnLooking.AddListener(OnLooking);
                }
                else
                {
                    _data.AppInput.OnLooking.RemoveListener(OnLooking);
                }
            }
        }

        public LookHandler(PlayerData data)
        {
            _data = data;
        }

        private void OnLooking(bool obj)
        {
            if (!_isEnable)
                return;

            Vector2 lookDirection = _data.AppInput.LookDirection;

            // Turn player
            _data.HorizontalAngle += lookDirection.x * _data.MouseHorizontalSensitivity;

            if (_data.HorizontalAngle > 360)
                _data.HorizontalAngle -= 360.0f;

            if (_data.HorizontalAngle < 0)
                _data.HorizontalAngle += 360.0f;

            Transform playerTransform = _data.CharacterController.transform;

            Vector3 currentAngles = playerTransform.localEulerAngles;
            currentAngles.y = _data.HorizontalAngle;
            playerTransform.localEulerAngles = currentAngles;

            // Camera look up/down
            float turnCam = -lookDirection.y;
            turnCam *= _data.MouseVerticalSensitivity;
            _data.VerticalAngle = Mathf.Clamp(turnCam + _data.VerticalAngle, -89.0f, 89.0f);

            Transform cameraTransform = _data.Camera.transform;
            currentAngles = cameraTransform.localEulerAngles;
            currentAngles.x = _data.VerticalAngle;
            cameraTransform.localEulerAngles = currentAngles;
        }
    }
}