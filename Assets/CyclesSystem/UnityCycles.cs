using App.SimplesScipts;
using System;
using UnityEngine;

namespace App.CyclesSystem
{
    public sealed class UnityCycles : MonoBehaviour
    {
        private readonly SEvent _updateCycle = new();

        private void Update()
        {
            _updateCycle.Invoke();
        }

        public void AddUpdate(Action action)
        {
            _updateCycle.AddListener(action);
        }
        public void RemoveUpdate(Action action)
        {
            _updateCycle.RemoveListener(action);
        }
    }
}