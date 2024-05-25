using App.AppInputSystem;

namespace App.Player.Private
{
    public sealed class CrosshairHandler
    {
        private readonly Data _data;
        private bool _isEnable;
        private bool _isCrosshairVisible;

        public bool IsEnable
        {
            get => _isEnable;
            set
            {
                if (value == _isEnable)
                    return;

                _isEnable = value;
                IWorldInput appInput = _data.AppInput;

                if (value)
                    _data.UnityCycles.AddUpdate(UpdateCycle);
                else
                    _data.UnityCycles.RemoveUpdate(UpdateCycle);
            }
        }


        public CrosshairHandler(Data data) 
            => _data = data;

        private void UpdateCycle()
        {
            bool isInputEnable = _data.AppInput.IsEnable;

            if (_isCrosshairVisible == isInputEnable)
                return;

            _isCrosshairVisible = isInputEnable;

            if (isInputEnable)
                _data.Aim.gameObject.SetActive(true);
            else
            {
                _data.AimHighlighter.SetActive(false);
                _data.Aim.gameObject.SetActive(false);
            }
        }

    }
}