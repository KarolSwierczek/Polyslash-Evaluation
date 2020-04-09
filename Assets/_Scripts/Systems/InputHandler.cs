namespace KarolSwierczek.Elevator.Systems
{
    using System;
    using UnityEngine;
    using Zenject;

    /// <summary>
    /// this class handles the player input and invokes appropriate events
    /// </summary>
    public sealed class InputHandler : ITickable
    {
        #region Public Types
        public sealed class OnPlayerInteractArgs : EventArgs
        {
        }
        #endregion Public Types

        #region Public Variables
        public event EventHandler<OnPlayerInteractArgs> OnPlayerInteract;
        #endregion Public Variables

        #region Private Methods
        void ITickable.Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnPlayerInteract?.Invoke(this, new OnPlayerInteractArgs());
            }
        }
        #endregion Private Methods
    }
}
