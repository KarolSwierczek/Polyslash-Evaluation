namespace KarolSwierczek.Elevator.Gameplay
{
    using UnityEngine;
    using Zenject;

    /// <summary>
    /// this class interrupts closing door animation and plays open animation seamlessly
    /// </summary>
    public sealed class DoorClosingInterrupt : StateMachineBehaviour
    {
        #region Public Methods
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            /* exit this state immediately if the door is blocked by player */
            if (_DoorBlocked) { animator.Play(_DoorOpenedState); return; }

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

        public void OnPlayerEnterCollider()
        {
            if (!_StateActive) { _DoorBlocked = true; return; }

            _CurrentAnimator.Play(_DoorOpeningState, 0, 1f - _CurrentStateInfo.normalizedTime);
        }

        public void OnPlayerExitCollider()
        {
            _DoorBlocked = false;
        }
        #endregion Public Methods

        #region Private Variables
        private Animator _CurrentAnimator;
        private AnimatorStateInfo _CurrentStateInfo;
        private bool _StateActive;
        private bool _DoorBlocked;

        private readonly int _DoorOpeningState = Animator.StringToHash("DoorOpening");
        private readonly int _DoorOpenedState = Animator.StringToHash("DoorOpened");
        #endregion Private Variables
    }
}
