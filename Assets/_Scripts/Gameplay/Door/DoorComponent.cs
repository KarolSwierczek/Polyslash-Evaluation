namespace KarolSwierczek.Elevator.Gameplay
{
    using UnityEngine;
    using Zenject;
    using Sirenix.OdinInspector;

    /// <summary>
    /// this class handles doors opening and closing
    /// </summary>
    public sealed class DoorComponent : MonoBehaviour
    {
        #region Unity Methods
        private void OnEnable()
        {
            _Controller.OnStopMoving += OnElevatorStopMoving;
        }

        private void OnDisable()
        {
            _Controller.OnStopMoving -= OnElevatorStopMoving;
        }
        #endregion Unity Methods

        #region Inspector Variables
        [FoldoutGroup("Refereces")]
        [SerializeField] private Animator _Animator;
        #endregion Inspector Variables

        #region Private Variables
        private const string _DoorOpenTrigger = "Open";

        [Inject] private readonly ElevatorController _Controller;
        #endregion Private Variables

        #region Private Methods
        private void OnElevatorStopMoving(object sender, ElevatorController.OnStopMovingArgs args)
        {
            _Animator.SetTrigger(_DoorOpenTrigger);
        }

        private void OnDoorAnimationComplete()
        {
            _Controller.OnDoorClosed();
        }
        #endregion Private Methods
    }
}
