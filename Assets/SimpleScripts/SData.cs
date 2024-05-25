using System;
using UnityEngine;

namespace App.SimplesScipts
{
    [Serializable]
    public struct SData<T1, T2>
    {
        [Header("Values")]
        [SerializeField] private T1 _value;
        [SerializeField] private T2 _value2;

        public readonly T1 Value => _value;
        public readonly T2 Value2 => _value2;
    }
}