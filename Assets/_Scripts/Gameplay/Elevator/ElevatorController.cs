namespace KarolSwierczek.Elevator.Gameplay
{
    using GameData;
    using System;
    using Zenject;
    using MEC;
    using System.Collections.Generic;

    /// <summary>
    /// this class controlls the elevators state and logic
    /// </summary>
    public sealed class ElevatorController
    {
        #region Public Types
        public enum ElevatorState
        {
            Idle,
            Busy,
            Moving,
        }
        public sealed class OnStopMovingArgs : EventArgs
        {
        }

        public sealed class OnStartMovingArgs : EventArgs
        {
            public float TargetHeight { get; }
            public float Speed { get; }
            
            public OnStartMovingArgs(float targetHeight, float speed)
            {
                TargetHeight = targetHeight;
                Speed = speed;
            }
        }
        #endregion Public Types

        #region Public Variables
        public event EventHandler<OnStopMovingArgs> OnStopMoving;
        public event EventHandler<OnStartMovingArgs> OnStartMoving;
        //note: more events can be added as the functionality expands

        public ElevatorState State { get; private set; } = ElevatorState.Idle;
        #endregion Public Variables

        #region Public Methods
        //note: I'm using a Zenject constructor in pure C# classes and field attributes in MonoBehaviours
        [Inject]
        public ElevatorController(ElevatorData data)
        {
            _Data = data;
        }

        public bool TryRequestFloor(int floor)
        {
            //cancel request if elevator is not idle
            if(State != ElevatorState.Idle) { return false; }

            Timing.RunCoroutine(OperationCoroutine(floor, _Data.Speed));
            return true;
        }

        public void OnFloorReached()
        {
            State = ElevatorState.Busy;
            OnStopMoving?.Invoke(this, new OnStopMovingArgs());
        }

        public void OnDoorClosed()
        {
            State = ElevatorState.Idle;
        }
        #endregion Public Methods

        #region Private Variables
        private readonly ElevatorData _Data;

        private int _CurrentFloor = 0;
        #endregion Private Variables

        #region Private Methods
        /// <summary>
        /// coroutine that gets the elevator to requested <paramref name="floor"/>
        /// with specified <paramref name="speed"/>
        /// </summary>
        private IEnumerator<float> OperationCoroutine(int floor, float speed)
        {
            State = ElevatorState.Busy;

            /* if the elevator is already on the requested floor */
            if(floor == _CurrentFloor)
            {
                OnStopMoving?.Invoke(this, new OnStopMovingArgs());
                yield return Timing.WaitUntilDone(DoorCoroutine());
            }

            /* if the elevator needs to move to requested floor */
            else
            {
                State = ElevatorState.Moving;

                var targetHeight = _Data.GetFloorHeight(floor);
                OnStartMoving?.Invoke(this, new OnStartMovingArgs(targetHeight, speed));

                yield return Timing.WaitUntilDone(MoveCoroutine());              
                yield return Timing.WaitUntilDone(DoorCoroutine());
            }

            _CurrentFloor = floor;
        }

        /// <summary>
        /// coroutine that waits for elevator movement to complete
        /// </summary>
        /// <remarks>
        /// coroutine is used insted of a simple variable for maintainability
        /// </remarks>
        private IEnumerator<float> MoveCoroutine()
        {
            while(State == ElevatorState.Moving) { yield return Timing.WaitForOneFrame; }
        }

        /// <summary>
        /// coroutine that waits for elevator doors to close
        /// </summary>
        /// <remarks>
        /// coroutine is used insted of a simple variable for maintainability
        /// </remarks>
        private IEnumerator<float> DoorCoroutine()
        {
            while (State == ElevatorState.Busy) { yield return Timing.WaitForOneFrame; }
        }
        #endregion
    }
}
