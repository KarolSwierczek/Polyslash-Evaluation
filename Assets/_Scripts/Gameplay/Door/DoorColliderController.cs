namespace KarolSwierczek.Elevator.Gameplay
{
    using UnityEngine;
    using Sirenix.OdinInspector;

    /// <summary>
    /// this class invokes an event when a player steps into the door collider
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public sealed class DoorColliderController : MonoBehaviour
    {
        #region Unity Methods
        private void Awake()
        {
            _InterruptStateBehavior = _DoorAnimator.GetBehaviour<DoorClosingInterrupt>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(_PlayerTag))
            {
                _InterruptStateBehavior.OnPlayerEnterCollider();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_PlayerTag))
            {
                _InterruptStateBehavior.OnPlayerExitCollider();
            }
        }
        #endregion Unity Methods

        #region Inspector Variables
        [FoldoutGroup("References")]
        [SerializeField] private Animator _DoorAnimator;
        #endregion Inspector Variables

        #region Private Variables
        private const string _PlayerTag = "player";
        private DoorClosingInterrupt _InterruptStateBehavior;
        #endregion Private Variables
    }
}