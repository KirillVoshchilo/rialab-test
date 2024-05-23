using UnityEngine;
using VContainer.Unity;
using VContainer;
using System.Collections.Generic;
using System;

namespace App.Runtime.Architecture.Scopes
{
    public abstract class Scope : MonoBehaviour
    {
        [SerializeField] private GameObject[] _autoInjectObjects;

        protected LifetimeScope _sceneScope;
        private readonly HashSet<IDisposable> _destructables = new();

        public LifetimeScope SceneScope { get => _sceneScope; set => _sceneScope = value; }

        public abstract void Build(IContainerBuilder builder);
        public abstract void Resolve(IObjectResolver container);

        protected void AutoInjectAll()
        {
            if (_autoInjectObjects == null)
                return;

            foreach (GameObject target in _autoInjectObjects)
            {
                if (target != null)
                {
                    _destructables.UnionWith(target.GetComponents<IDisposable>());
                    _destructables.UnionWith(target.GetComponentsInChildren<IDisposable>());
                    _sceneScope.Container.InjectGameObject(target);
                }
            }
        }
    }
}