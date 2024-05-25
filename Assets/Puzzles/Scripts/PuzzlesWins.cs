
using App.SimplesScipts;

namespace App.Puzzles
{
    public class PuzzlesWins
    {
        private int _winsCount;
        private SEvent<int> _onWinsCountChanged = new();

        public int WinsCount
        {
            get => _winsCount;
            set
            {
                if (_winsCount == value)
                    return;

                _winsCount = value;
                _onWinsCountChanged.Invoke(value);
            }
        }
        public SEvent<int> OnWinsCountChanged => _onWinsCountChanged;
    }
}