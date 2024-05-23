using App.Runtime.Architecture;
using App.Runtime.Architecture.AppInputSystem;
using App.Runtime.Content.Player.Private;
using UnityEngine;
using VContainer;

namespace App.Runtime.Content.Player
{
    public sealed class PlayerEntity : MonoBehaviour, IEntity
    {
        [SerializeField] private PlayerData _data;

        private LookHandler _lookHandler;
        private JumpHandler _jumpHandler;
        private WalkHandler _walkHandler;
        private GravityHandler _gravityHandler;
        private InteractionHandler _interactionHandler;

        public bool IsEnable
        {
            get => _data.IsEnable;
            set
            {
                if (value == _data.IsEnable)
                    return;

                _gravityHandler.IsEnable = value;
                _jumpHandler.IsEnable = value;
                _lookHandler.IsEnable = value;
                _walkHandler.IsEnable = value;
            }
        }

        [Inject]
        public void Construct(UnityCycles unityCycles,
            IAppInput appInput)
        {
            _data.AppInput = appInput;
            _data.UnityCycles = unityCycles;
            _data.PlayerEntity = this;

            _walkHandler = new WalkHandler(_data);
            _jumpHandler = new JumpHandler(_data);
            _lookHandler = new LookHandler(_data);
            _gravityHandler = new GravityHandler(_data);

            _data.VerticalAngle = 0.0f;
            _data.HorizontalAngle = transform.localEulerAngles.y;
        }

        public T Get<T>() where T : class
        {
            if (typeof(T) == typeof(Flags))
                return _data.Flags as T;

            return null;
        }
    }
}