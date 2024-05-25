using App.AppInputSystem;
using App.CyclesSystem;
using Cinemachine;
using System;
using UnityEngine;

namespace App.Player.Private
{
    [Serializable]
    public sealed class Data
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private CinemachineVirtualCamera _cinemachineCamera;
        [SerializeField] private RectTransform _aim;
        [SerializeField] private GameObject _aimHighlighter;

        [Header("Control Settings")]
        [SerializeField] private float _mouseHorizontalSensitivity = 100.0f;
        [SerializeField] private float _mouseVerticalSensitivity = 100.0f;
        [SerializeField] private float _playerSpeed = 5.0f;
        [SerializeField] private float _jumpSpeed = 5.0f;

        private IWorldInput _appInput;
        private UnityCycles _unityCycles;
        private PlayerEntity _playerEntity;
        private Camera _mainCamera;
        private float _verticalSpeed = 0.0f;
        private float _verticalAngle;
        private float _horizontalAngle;
        private float _speed = 0f;
        private bool _isGrounded;
        private Vector3 _horizontalVelocity;

        public CharacterController CharacterController => _characterController;
        public CinemachineVirtualCamera Camera => _cinemachineCamera;
        public float MouseVerticalSensitivity => _mouseVerticalSensitivity;
        public float PlayerSpeed => _playerSpeed;
        public float JumpSpeed => _jumpSpeed;

        public float VerticalSpeed { get => _verticalSpeed; set => _verticalSpeed = value; }
        public float VerticalAngle { get => _verticalAngle; set => _verticalAngle = value; }
        public float HorizontalAngle { get => _horizontalAngle; set => _horizontalAngle = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public bool IsGrounded { get => _isGrounded; set => _isGrounded = value; }
        public UnityCycles UnityCycles { get => _unityCycles; set => _unityCycles = value; }
        public Vector3 HorizontalVelocity { get => _horizontalVelocity; set => _horizontalVelocity = value; }
        public PlayerEntity PlayerEntity { get => _playerEntity; set => _playerEntity = value; }
        public IWorldInput AppInput { get => _appInput; set => _appInput = value; }
        public float MouseHorizontalSensitivity => _mouseHorizontalSensitivity;

        public RectTransform Aim => _aim;
        public GameObject AimHighlighter => _aimHighlighter;

        public Camera MainCamera { get => _mainCamera; set => _mainCamera = value; }
    }
}