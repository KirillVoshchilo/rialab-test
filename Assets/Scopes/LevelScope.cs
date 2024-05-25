using UnityEngine;
using VContainer;
using VContainer.Unity;
using App.Player;
using App.CyclesSystem;
using App.Puzzles;

namespace App.Scopes
{
    public sealed class LevelScope : Scope
    {
        [SerializeField] private PlayerEntity _playerEntity;
        [SerializeField] private UnityCycles _unityCycles;

        public override void Build(IContainerBuilder builder)
        {
            builder.RegisterComponent(_playerEntity);
            builder.RegisterComponent(_unityCycles);

            builder.Register<PuzzlesWins>(Lifetime.Singleton);

        }
        public override void Resolve(IObjectResolver container)
        {
            container.Resolve<PlayerEntity>();

            AutoInjectAll();
            StartLevel();
        }

        private void StartLevel()
        {
            _playerEntity.IsEnable = true;
         
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
