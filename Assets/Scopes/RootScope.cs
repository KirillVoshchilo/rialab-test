using App.AppInputSystem;
using App.SceneLoaderSystem;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace App.Scopes
{
    public sealed class RootScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<WorldInput>(Lifetime.Singleton)
                .As<IWorldInput>();
            builder.Register<PuzzleInput>(Lifetime.Singleton)
                .As<IPuzzleInput>();

            SceneLoader loadSceneController = new(this);
            builder.RegisterInstance(loadSceneController)
                .AsSelf();

            builder.RegisterBuildCallback(OnRegistrationEnded);
        }

        private void OnRegistrationEnded(IObjectResolver resolver)
        {
            IWorldInput worldInput = resolver.Resolve<IWorldInput>();
            worldInput.IsEnable = true;

            SceneLoader loadSceneController = resolver.Resolve<SceneLoader>();
            loadSceneController.Load(SceneIndexes.MAIN_MENU_SCENE)
                .Forget();
        }
    }
}