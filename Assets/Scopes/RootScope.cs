using App.Runtime.Architecture.AppInputSystem;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace App.Runtime.Architecture.Scopes
{
    public sealed class RootScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<AppInput>(Lifetime.Singleton)
                .As<IAppInput>();

            SceneLoader loadSceneController = new(this);
            builder.RegisterInstance(loadSceneController)
                .AsSelf();

            builder.RegisterBuildCallback(OnRegistrationEnded);
        }

        private void OnRegistrationEnded(IObjectResolver resolver)
        {
            IAppInput appInput = resolver.Resolve<IAppInput>();
            appInput.IsEnable = true;

            SceneLoader loadSceneController = resolver.Resolve<SceneLoader>();
            loadSceneController.Load(SceneIndexes.MAIN_MENU_SCENE)
                .Forget();
        }
    }
}