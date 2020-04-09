namespace KarolSwierczek.Elevator.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;
    using Zenject;
    using MEC;
    using Sirenix.OdinInspector;

    /// <summary>
    /// this class handles moving the elevator up and down
    /// </summary>
    public sealed class ElevatorComponent : MonoBehaviour
    {
        #region Unity Methods
        private void OnEnable()
        {
            _Controller.OnStartMoving += OnElevatorStartMoving;
            _Controller.OnStopMoving += OnElevatorStopMoving;

            _AudioSourceMovement.clip = _WhileMoving;
        }

        private void OnDisable()
        {
            _Controller.OnStartMoving -= OnElevatorStartMoving;
            _Controller.OnStopMoving -= OnElevatorStopMoving;
        }
        #endregion Unity Methods

        #region Inspector Variables
        [FoldoutGroup("Settings"), BoxGroup("Settings/Audio"), Tooltip("Clip that plays when the elevator starts moving")]
        [SerializeField] private AudioClip _StartMoving;

        [FoldoutGroup("Settings"), BoxGroup("Settings/Audio"), Tooltip("Clip that plays when the elevator stops moving")]
        [SerializeField] private AudioClip _StopMoving;

        [FoldoutGroup("Settings"), BoxGroup("Settings/Audio"), Tooltip("Clip that plays while the elevator is moving")]
        [SerializeField] private AudioClip _WhileMoving;

        [FoldoutGroup("References")]
        [SerializeField] private AudioSource _AudioSourceEffects;

        [FoldoutGroup("References")]
        [SerializeField] private AudioSource _AudioSourceMovement;
        #endregion Inspector Variables

        #region Private Variables
        [Inject] private readonly ElevatorController _Controller;
        #endregion Private Variables

        #region Private Methods
        private void OnElevatorStartMoving(object sender, ElevatorController.OnStartMovingArgs args)
        {
            Timing.RunCoroutine(MoveElevatorCoroutine(args.TargetHeight, args.Speed));

            _AudioSourceMovement.Play();
            _AudioSourceEffects.clip = _StartMoving;
            _AudioSourceEffects.Play();
        }

        private void OnElevatorStopMoving(object sender, ElevatorController.OnStopMovingArgs args)
        {
            _AudioSourceMovement.Stop();
            _AudioSourceEffects.clip = _StopMoving;
            _AudioSourceEffects.Play();
        }

        /// <summary>
        /// moves the elevator to <paramref name="targetHeight"/> with specified <paramref name="speed"/>
        /// </summary>
        private IEnumerator<float> MoveElevatorCoroutine(float targetHeight, float speed)
        {
            var time = 0f;
            var startPosition = transform.position;
            var targetPosition = new Vector3(startPosition.x, targetHeight, startPosition.z);
            var shift = (targetPosition - startPosition).magnitude;
            var duration = shift / speed;

            while(time <= duration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
                time += Time.deltaTime;
                yield return Timing.WaitForOneFrame;
            }

            _Controller.OnFloorReached();
        }
        #endregion Private Methods
    }
}
