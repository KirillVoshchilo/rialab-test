using App.Runtime.Architecture.AppInputSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Runtime.Content.Player.Private
{
    public sealed class WalkHandler
    {
        private readonly PlayerData _data;

        private bool _alreadyMoving;
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
                    _data.AppInput.OnMoving.AddListener(OnMoving);
                }
                else
                {
                    _data.AppInput.OnMoving.RemoveListener(OnMoving);
                }
            }
        }

        public WalkHandler(PlayerData data)
        {
            _data = data;
        }


        private void OnMoving(bool obj)
        {
            if (_alreadyMoving || !obj)
                return;

            MoveProcess()
                .Forget();
        }
        private async UniTask MoveProcess()
        {
            IAppInput appInput = _data.AppInput;

            while (_isEnable && _data.AppInput.IsMoving)
            {
                _alreadyMoving = true;

                CharacterController controller = _data.CharacterController;
                Transform playerTransform = controller.transform;
                _data.Speed = 0;

                Vector3 moveDirection = new(appInput.MoveDirection.x, 0, appInput.MoveDirection.y);
       
                _data.Speed = _data.PlayerSpeed;

                Vector3 move = Time.deltaTime * _data.Speed * moveDirection;
                move = playerTransform.TransformDirection(move);

                controller.Move(move);

                _data.HorizontalVelocity = move;

                await UniTask.NextFrame();
            }
            _data.HorizontalVelocity = Vector3.zero;
            _alreadyMoving = false;
        }
    }
}