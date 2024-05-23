using App.Runtime.Content.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Runtime.Architecture.Scopes
{
    public sealed class MainMenuSceneScope : Scope
    {
        [SerializeField] private MainMenuUI _mainMenuUI;

        public override void Build(IContainerBuilder builder)
        {
            builder.RegisterComponent(_mainMenuUI);
        }

        public override void Resolve(IObjectResolver container)
        {
            container.Resolve<MainMenuUI>();

            AutoInjectAll();
        }
    }
}