using System;
using UnityEngine;

namespace App.SimplesScipts
{
    [Serializable]
    public sealed class Flags
    {
        [SerializeField] private EFlags[] _flags;

        public EFlags[] Values => _flags;
    }
}