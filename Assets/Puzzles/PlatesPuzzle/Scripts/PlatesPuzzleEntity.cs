using App.AppInputSystem;
using App.Puzzles.PlatesPuzzle.Private;
using App.SimplesScipts;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace App.Puzzles.PlatesPuzzle
{
    public class PlatesPuzzleEntity : MonoBehaviour, IEntity
    {
        [SerializeField] private Data _data;

        private bool _isEnable;

        [Inject]
        public void Construct(IWorldInput worldInput,
            IPuzzleInput puzzleInput,
            PuzzlesWins puzzlesWins)
        {
            _data.InteractionObject = new InteractionObject(InteractiWithPuzzle);
            _data.WorldInput = worldInput;
            _data.PuzzleInput = puzzleInput;
            _data.PuzzleWins = puzzlesWins;
            _data.MainCamera = Camera.main;
            _data.CinemachineBrain = _data.MainCamera.transform.GetComponent<CinemachineBrain>();

            ResetValues();
        }
        public T Get<T>() where T : class
        {
            if (typeof(T) == typeof(InteractionObject))
                return _data.InteractionObject as T;

            return null;
        }

        private void InteractiWithPuzzle()
        {
            if (_data.IsWinned)
                return;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            _data.Shutdown—ollider.enabled = false;

            _isEnable = true;
            _data.VirtualCamera.Priority = 20;
            _data.PuzzleInput.IsEnable = true;
            _data.WorldInput.IsEnable = false;

            _data.PuzzleInput.OnClicked.AddListener(TryGrab);
        }
        [Button("Reset", ButtonSizes.Large), GUIColor(0.4f, 0.8f, 1)]
        private void ResetValues()
        {
            if (_data.PuzzleWins == null)
                return;

            int count = _data.Circles.Length;

            for (int i = 0; i < count; i++)
            {
                int randomAngle = Random.Range(0, 360);
                Quaternion rotation = Quaternion.AngleAxis(randomAngle, _data.Circles[i].transform.up);
                Vector3 vector = _data.Circles[i].transform.forward;
                vector = rotation * vector;

                _data.Circles[i].transform.rotation = Quaternion.LookRotation(vector, _data.Circles[i].transform.up);
            }
            if (_data.IsWinned)
            {
                _data.IsWinned = false;
                _data.PuzzleWins.WinsCount -= 1;
            }
        }
        private bool CheckForWin()
        {
            int count = _data.Circles.Length;
            int result = 0;

            for (int i = 0; i < count; i++)
            {
                float angle = Vector3.Angle(_data.Circles[i].transform.forward, Vector3.up);

                if (Mathf.Abs(angle) <= 3)
                    result++;
            }

            return result == count;
        }
        private void TryGrab(bool obj)
        {
            if (!obj)
                return;

            Vector2 pointerPosition = _data.PuzzleInput.PointerPosition;
            Ray ray = _data.MainCamera.ScreenPointToRay(pointerPosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHitInfo))
            {
                int count = _data.Circles.Length;

                for (int i = 0; i < count; i++)
                {
                    if (_data.Circles[i] == raycastHitInfo.collider.gameObject)
                    {
                        _data.GrabbedCircle = _data.Circles[i];
                        break;
                    }
                }
            }

            if (_data.GrabbedCircle != null)
            {
                MovingCircleProcess()
                    .Forget();
            }
        }
        private async UniTask MovingCircleProcess()
        {
            Transform rotationObject = _data.GrabbedCircle.transform;
            Vector2 screenRotationCenter = _data.MainCamera.WorldToScreenPoint(rotationObject.position);

            while (_isEnable && _data.PuzzleInput.IsPressed)
            {
                Vector2 pointerPosition = _data.PuzzleInput.PointerPosition;
                Vector2 screenDirection = pointerPosition - screenRotationCenter;
                Vector3 direction = new(0, screenDirection.y, screenDirection.x);
                rotationObject.rotation = Quaternion.LookRotation(direction, rotationObject.up);

                if (CheckForWin())
                {
                    _data.IsWinned = true;
                    _data.PuzzleWins.WinsCount += 1;
                    FinishPuzzle();
                }

                await UniTask.NextFrame();
            }
            _data.GrabbedCircle = null;
        }
        [Button("FinishPuzzle", ButtonSizes.Large), GUIColor(0.8f, 0.3f, 0.3f)]
        private async void FinishPuzzle()
        {
            if (_data.PuzzleInput == null)
                return;

            _isEnable = false;

            _data.Shutdown—ollider.enabled = true;

            _data.VirtualCamera.Priority = 5;
            _data.PuzzleInput.IsEnable = false;
            _data.IsWinned = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            await UniTask.WaitUntil(() => _data.CinemachineBrain.IsBlending == false);

            _data.WorldInput.IsEnable = true;
        }
    }
}