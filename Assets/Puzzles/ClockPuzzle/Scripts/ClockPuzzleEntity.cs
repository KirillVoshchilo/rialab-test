using UnityEngine;
using VContainer;
using App.Puzzles.ClockPuzzle.Private;
using Cysharp.Threading.Tasks;
using App.SimplesScipts;
using App.AppInputSystem;

namespace App.Puzzles.ClockPuzzle
{
    public class ClockPuzzleEntity : MonoBehaviour, IEntity
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

            _data.TimerField.text = $"{_data.Hours}:{_data.Minutes}";
        }

        private void InteractiWithPuzzle()
        {
            if (_data.IsWinned)
                return;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            _isEnable = true;
            _data.VirtualCamera.Priority = 20;
            _data.PuzzleInput.IsEnable = true;
            _data.WorldInput.IsEnable = false;
            _data.PuzzleInput.OnClicked.AddListener(StartMoving);
        }
        private bool CheckForWin()
        {
            Vector3 minDirection = _data.LongArrow.transform.parent.forward;
            Vector3 hourDirection = _data.ShortArrow.transform.parent.forward;

            float minAngle = Vector3.Angle(minDirection, Vector3.up);
            float hoursAngle = Vector3.Angle(hourDirection, Vector3.up);

            float minutesPart = Mathf.Sign(Vector3.Dot(minDirection, _data.RotationCenter.forward));
            float hoursPart = Mathf.Sign(Vector3.Dot(hourDirection, _data.RotationCenter.forward));

            float h, m;

            if (hoursPart >= 0)
                h = 6 * hoursAngle / 180;
            else
                h = 6 + (6 * (180 - hoursAngle) / 180);

            if (minutesPart >= 0)
                m = 30 * minAngle / 180;
            else
                m = 30 + (30 * (180 - minAngle) / 180);

            bool hGood = false;
            bool mGood = false;

            if (Mathf.Abs(_data.Hours - h) <= 0.5 || Mathf.Abs(_data.Hours - h - 12) <= 0.5)
                hGood = true;

            if (Mathf.Abs(_data.Minutes - m) <= 2)
                mGood = true;

            Debug.Log($"min {mGood} h {hGood}");

            return mGood && hGood;
        }

        private void StartMoving(bool obj)
        {
            if (!obj)
                return;

            Vector2 pointerPosition = _data.PuzzleInput.PointerPosition;
            Ray ray = _data.MainCamera.ScreenPointToRay(pointerPosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHitInfo))
            {
                if (_data.ShortArrow == raycastHitInfo.collider.gameObject)
                {
                    _data.GrabbedArrow = _data.ShortArrow;
                }

                else if (_data.LongArrow == raycastHitInfo.collider.gameObject)
                {
                    _data.GrabbedArrow = _data.LongArrow;
                }
            }


            if (_data.GrabbedArrow != null)
            {
                MovingArrowProcess()
                    .Forget();
            }
        }

        private async UniTask MovingArrowProcess()
        {
            Transform rotationObject = _data.GrabbedArrow.transform.parent.transform;
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
            _data.GrabbedArrow = null;
        }

        private void FinishPuzzle()
        {
            _isEnable = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _data.VirtualCamera.Priority = 5;
            _data.PuzzleInput.IsEnable = false;
            _data.WorldInput.IsEnable = true;
            _data.IsWinned = true;
            _data.PuzzleWins.WinsCount += 1;
        }

        public T Get<T>() where T : class
        {
            if (typeof(T) == typeof(InteractionObject))
                return _data.InteractionObject as T;


            return null;
        }
    }
}