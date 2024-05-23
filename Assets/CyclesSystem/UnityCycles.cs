using App.Runtime.Simples;
using System;
using UnityEngine;

namespace App.Runtime.Architecture
{
    public sealed class UnityCycles : MonoBehaviour
    {
        private readonly SEvent _updateCycle = new();
        private readonly SEvent _enableCycle = new();
        private readonly SEvent _disableCycle = new();

        private void Update()
        {
            _updateCycle.Invoke();
        }
        private void OnEnable()
        {
            _enableCycle.Invoke();
        }
        private void OnDisable()
        {
            _disableCycle.Invoke();
        }

        public void AddUpdate(Action action)
        {
            _updateCycle.AddListener(action);
        }
        public void RemoveUpdate(Action action)
        {
            _updateCycle.RemoveListener(action);
        }
        public void AddEnable(Action action)
        {
            _enableCycle.AddListener(action);
        }
        public void RemoveEnable(Action action)
        {
            _enableCycle.RemoveListener(action);
        }
        public void AddDisable(Action action)
        {
            _disableCycle.AddListener(action);
        }
        public void RemoveDisable(Action action)
        {
            _disableCycle.RemoveListener(action);
        }
    }
}