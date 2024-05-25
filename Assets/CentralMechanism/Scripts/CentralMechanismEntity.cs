using App.CentralMechanism.Private;
using App.Puzzles;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace App.CentralMechanism
{
    public sealed class CentralMechanismEntity : MonoBehaviour
    {
        [SerializeField] private Data _data;

        [Inject]
        public void Construct(PuzzlesWins puzzlesWins)
        {
            _data.PuzzleWins = puzzlesWins;
            puzzlesWins.OnWinsCountChanged.AddListener(OnWinsCountChanged);
        }

        private void OnWinsCountChanged(int winsCount)
        {
            if (winsCount >= 1)
                Move_1();
            else
                Stop_1();


            if (winsCount >= 2)
                Move_2();
            else
                Stop_2();

            if (winsCount >= 3)
                Move_3();
            else
                Stop_3();
        }

        private void Move_1()
        {
            Transform tr = _data.Details[0].transform;

            tr.DORotate(new Vector3(0, 0, 90), 1)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);
        }
        private void Stop_1()
        {
            Transform tr = _data.Details[0].transform;

            tr.DOKill();
        }
        private void Move_2()
        {
            Transform tr = _data.Details[1].transform;

            int count = _data.Path.Length;
            Vector3[] path = new Vector3[count];

            for (int i = 0; i < count; i++)
                path[i] = _data.Path[i].position;

            tr.DOPath(path, 4, PathType.Linear)
                .SetOptions(true)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
        }
        private void Stop_2()
        {
            Transform tr = _data.Details[1].transform;

            tr.DOKill();
        }
        private void Move_3()
        {
            Transform tr = _data.Details[2].transform;

            tr.DOScale(0.5f, 1)
                .SetEase(Ease.InQuad)
                .SetLoops(-1, LoopType.Yoyo);
        }
        private void Stop_3()
        {
            Transform tr = _data.Details[2].transform;

            tr.DOKill();
        }
    }
}