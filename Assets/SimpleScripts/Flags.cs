using App.Runtime.Architecture;
using System;
using UnityEngine;

namespace App.Runtime.Content
{
    [Serializable]
    public sealed class Flags
    {
        [SerializeField] private EFlags[] _flags;

        public EFlags[] Values => _flags;
    }
}