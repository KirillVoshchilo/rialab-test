using App.SimplesScipts;

namespace App.Puzzles
{
    public sealed class PuzzlesWins
    {
        private readonly SEvent<int> _onWinsCountChanged = new();
        
        private int _winsCount;

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
        public ISEvent<int> OnWinsCountChanged => _onWinsCountChanged;
    }
}