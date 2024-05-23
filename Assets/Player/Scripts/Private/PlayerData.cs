using App.Runtime.Architecture;
using App.Runtime.Architecture.AppInputSystem;
using Cinemachine;
using System;
using UnityEngine;

namespace App.Runtime.Content.Player.Private
{
    [Serializable]
    public sealed class PlayerData
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private CinemachineVirtualCamera _cinemachineCamera;
        [SerializeField] private Flags _flags;

        [Header("Control Settings")]
        [SerializeField] private float _mouseHorizontalSensitivity = 100.0f;
        [SerializeField] private float _mouseVerticalSensitivity = 100.0f;
        [SerializeField] private float _playerSpeed = 5.0f;
        [SerializeField] private float _jumpSpeed = 5.0f;

        private IAppInput _appInput;
        private UnityCycles _unityCycles;
        private PlayerEntity _playerEntity;
        private float _verticalSpeed = 0.0f;
        private bool _isEnable = false;
        private float _verticalAngle;
        private float _horizontalAngle;
        private bool _isGrounded;
        private float _speed = 0f;
        private Vector3 _horizontalVelocity;

        public CharacterController CharacterController => _characterController;
        public CinemachineVirtualCamera Camera => _cinemachineCamera;
        public float MouseVerticalSensitivity => _mouseVerticalSensitivity;
        public float PlayerSpeed => _playerSpeed;
        public float JumpSpeed => _jumpSpeed;

        public float VerticalSpeed { get => _verticalSpeed; set => _verticalSpeed = value; }
        public bool IsEnable
        {
            get => _isEnable;
            set
            {
                if (value == _isEnable)
                    return;

                _isEnable = value;
            }
        }
        public float VerticalAngle { get => _verticalAngle; set => _verticalAngle = value; }
        public float HorizontalAngle { get => _horizontalAngle; set => _horizontalAngle = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public bool IsGrounded { get => _isGrounded; set => _isGrounded = value; }
        public UnityCycles UnityCycles { get => _unityCycles; set => _unityCycles = value; }
        public Vector3 HorizontalVelocity { get => _horizontalVelocity; set => _horizontalVelocity = value; }
        public PlayerEntity PlayerEntity { get => _playerEntity; set => _playerEntity = value; }
        public IAppInput AppInput { get => _appInput; set => _appInput = value; }
        public float MouseHorizontalSensitivity => _mouseHorizontalSensitivity;
        public Flags Flags => _flags;
    }
}