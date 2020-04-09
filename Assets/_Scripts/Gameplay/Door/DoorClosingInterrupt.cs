namespace KarolSwierczek.Elevator.Gameplay
{
    using Sirenix.OdinInspector;
    using UnityEngine;

    /// <summary>
    /// this class interrupts closing door animation and plays open animation seamlessly
    /// </summary>
    public sealed class DoorClosingInterrupt : StateMachineBehaviour
    {
        #region Public Methods
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _CurrentAnimator = animator;
            _StateActive = true;
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _StateActive = false;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _CurrentStateInfo = stateInfo;
        }
        #endregion Public Methods

        #region Unity Methods
        private void OnEnable()
        {
            _DoorCollider.OnPlayerEnter += OnPlayerEnterCollider;
        }

        private void OnDisable()
        {
            _DoorCollider.OnPlayerEnter -= OnPlayerEnterCollider;
        }
        #endregion Unity Methods

        #region Inspector Variables
        [FoldoutGroup("Refereces")]
        [SerializeField] private DoorColliderController _DoorCollider;
        #endregion Inspector Variables

        #region Private Variables
        private Animator _CurrentAnimator;
        private AnimatorStateInfo _CurrentStateInfo;
        private bool _StateActive;

        private const string _DoorOpeningStateName = "DoorOpening";
        #endregion Private Variables

        #region Private Methods
        private void OnPlayerEnterCollider(object sender, DoorColliderController.OnPlayerEnterArgs args)
        {
            if (!_StateActive) { return; }
            _CurrentAnimator.Play(_DoorOpeningStateName, 0, 1f - _CurrentStateInfo.normalizedTime);
        }
        #endregion Private Methods
    }
}
