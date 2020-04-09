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
        #endregion Public Types

        #region Public Variables
        public event EventHandler<OnPlayerEnterArgs> OnPlayerEnter;
        #endregion Public Variables

        #region Unity Methods
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(_PlayerTag))
            {
                OnPlayerEnter?.Invoke(this, new OnPlayerEnterArgs());
            }
        }
        #endregion Unity Methods

        #region Private Variables
        private const string _PlayerTag = "player";
        #endregion Private Variables
    }
}