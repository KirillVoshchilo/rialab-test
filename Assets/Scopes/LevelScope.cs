using UnityEngine;
using VContainer;
using App.Runtime.Content.Player;
using VContainer.Unity;

namespace App.Runtime.Architecture.Scopes
{
    public sealed class LevelScope : Scope
    {
        [SerializeField] private PlayerEntity _playerEntity;
        [SerializeField] private UnityCycles _unityCycles;

        public override void Build(IContainerBuilder builder)
        {
            builder.RegisterComponent(_playerEntity);

            builder.RegisterComponent(_unityCycles);

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
        }
    }
}
