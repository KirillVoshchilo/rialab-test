using App.Runtime.Architecture.Scopes;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace App.Runtime.Architecture
{
    public sealed class SceneLoader
    {
        private int _currentLoadedLevel;
        private bool _levelIsLoaded;
        private LifetimeScope _rootScope;
        private Scope _sceneScope;

        public SceneLoader(LifetimeScope rootLifetimeScope)
        {
            _rootScope = rootLifetimeScope;
        }

        public bool LevelIsLoaded => _levelIsLoaded;
        public int CurrentLoadedLevel => _currentLoadedLevel;

        public async UniTask Load(int sceneID)
        {
            UnloadPreviousScope();

            await SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Single);
            _currentLoadedLevel = sceneID;
            _sceneScope = GameObject.FindAnyObjectByType<Scope>();
            LifetimeScope lifetimeScope = _rootScope.CreateChild(_sceneScope.Build);
            _sceneScope.SceneScope = lifetimeScope;
            _sceneScope.Resolve(lifetimeScope.Container);
        }

        private void UnloadPreviousScope()
        {
            if (_sceneScope == null)
                _sceneScope = GameObject.FindAnyObjectByType<Scope>();

            if (_sceneScope == null)
                return;

            _sceneScope.SceneScope.Dispose();
            Resources.UnloadUnusedAssets();
        }
    }
}