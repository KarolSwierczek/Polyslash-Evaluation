namespace KarolSwierczek.Elevator.Gameplay
{
    using UnityEngine;
    using System;

    /// <summary>
    /// this class invokes an event when a player steps into the door collider
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public sealed class DoorColliderController : MonoBehaviour
    {
        #region Public Types
        public sealed class OnPlayerEnterArgs : EventArgs
        {
        }

        public sealed class OnPlayerExitArgs : EventArgs
        {
        }
        #endregion Public Types

        #region Public Variables
        public event EventHandler<OnPlayerEnterArgs> OnPlayerEnter; 
        public event EventHandler<OnPlayerExitArgs> OnPlayerExit;
        #endregion Public Variables

        #region Unity Methods
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(_PlayerTag))
            {
                OnPlayerEnter?.Invoke(this, new OnPlayerEnterArgs());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_PlayerTag))
            {
                OnPlayerExit?.Invoke(this, new OnPlayerExitArgs());
            }
        }
        #endregion Unity Methods

        #region Private Variables
        private const string _PlayerTag = "player";
        #endregion Private Variables
    }
}