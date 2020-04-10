namespace KarolSwierczek.Elevator.Gameplay
{
    using Sirenix.OdinInspector;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    public sealed class FloorContainter : MonoBehaviour
    {
        #region Public Methods
        public float GetFloorHeight(int floor)
        {
            if (floor >= _Floors.Count) { throw new ArgumentException("Floor: " + floor + "is not defined!"); }
            return _Floors[floor].GetHeight();
        }
        #endregion Public Methods

        #region Inspector Variables
        [ListDrawerSettings(CustomAddFunction ="AddFloor")]
        [SerializeField] private List<Floor> _Floors;
        #endregion Inspector Variables

        #region Private Types
        [Serializable]
        private class Floor
        {
            #region Public Variables
            public Transform FloorTransform;
            public bool UseCustomHeight;
            [ShowIf("UseCustomHeight")] public float CustomHeight;
            #endregion Public Variables

            #region Public Methods
            public Floor(int floorNumber)
            {
                _FloorNumber = floorNumber;
            }

            public float GetHeight()
            {
                return UseCustomHeight ? CustomHeight : FloorTransform.position.y;
            }
            #endregion Public Methods

            #region Private Variables
            [ShowInInspector, ReadOnly]
            private readonly int _FloorNumber;
            #endregion Private Variables
        }
        #endregion Private Types

        #region Private Methods
        private void AddFloor()
        {
            _Floors.Add(new Floor(_Floors.Count));
        }
        #endregion Private Methods
    }
}
