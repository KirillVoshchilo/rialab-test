using App.AppInputSystem;
using App.Puzzles;
using App.SimplesScipts;
using UnityEngine;

namespace App.Player.Private
{
    public sealed class InteractionHandler
    {
        private readonly Data _data;
        private InteractionObject _interactionObject;
        private bool _isEnable;
        private bool _canInteract;

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
                {
                    _data.AppInput.OnInteractionPressed.AddListener(OnInteractionPressed);
                    _data.UnityCycles.AddUpdate(UpdateCycle);
                }
                else
                {
                    _data.UnityCycles.RemoveUpdate(UpdateCycle);
                    _data.AppInput.OnInteractionPressed.RemoveListener(OnInteractionPressed);
                }
            }
        }


        public InteractionHandler(Data data)
        {
            _data = data;
        }
        private void OnInteractionPressed()
        {
            if (_canInteract)
                _interactionObject.Interact();
        }

        private void UpdateCycle()
        {
            if (!_data.AppInput.IsEnable)
                return;

            _canInteract = CheckForInteractableObject(out InteractionObject interactionObject);

            if (_canInteract)
                _data.AimHighlighter.Highlight();
            else
                _data.AimHighlighter.TurnOffHighlight();

            _interactionObject = interactionObject;
        }

        private bool CheckForInteractableObject(out InteractionObject interactionObject)
        {
            interactionObject = null;

            Vector3 originPoint = _data.MainCamera.ViewportToWorldPoint(_data.Aim.position);
            Ray ray = new(originPoint, _data.MainCamera.transform.forward);

            if (!Physics.Raycast(ray, out RaycastHit hit, 2))
                return false;

            IEntity entity = hit.collider.gameObject.GetComponent<IEntity>();

            if (entity == null)
                return false;

            interactionObject = entity.Get<InteractionObject>();

            if (interactionObject == null)
                return false;

            return true;
        }
    }
}