using System;

namespace App.Puzzles
{
    public class InteractionObject
    {
        private Action _action;

        public InteractionObject(Action interaction)
            => _action = interaction;

        public void Interact()
            => _action.Invoke();
    }
}