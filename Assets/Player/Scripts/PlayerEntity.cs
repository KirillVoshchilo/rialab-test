using App.AppInputSystem;
using App.CyclesSystem;
using App.Player.Private;
using App.SimplesScipts;
using UnityEngine;
using VContainer;

namespace App.Player
{
    public sealed class PlayerEntity : MonoBehaviour, IEntity
    {
        [SerializeField] private Data _data;

        private LookHandler _lookHandler;
        private JumpHandler _jumpHandler;
        private WalkHandler _walkHandler;
        private GravityHandler _gravityHandler;
        private InteractionHandler _interactionHandler;
        private CrosshairHandler _crosshairHandler;

        private bool _isEnable;

        public bool IsEnable
        {
            get => _isEnable;
            set
            {
                if (value == _isEnable)
                    return;

                _crosshairHandler.IsEnable = value;
                _gravityHandler.IsEnable = value;
                _jumpHandler.IsEnable = value;
                _lookHandler.IsEnable = value;
                _walkHandler.IsEnable = value;
                _interactionHandler.IsEnable = value;
            }
        }

        [Inject]
        public void Construct(UnityCycles unityCycles,
            IWorldInput appInput)
        {
            _data.AppInput = appInput;
            _data.UnityCycles = unityCycles;
            _data.PlayerEntity = this;
            _data.MainCamera = Camera.main;

            _walkHandler = new WalkHandler(_data);
            _jumpHandler = new JumpHandler(_data);
            _lookHandler = new LookHandler(_data);
            _gravityHandler = new GravityHandler(_data);
            _interactionHandler = new InteractionHandler(_data);
            _crosshairHandler = new CrosshairHandler(_data);

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