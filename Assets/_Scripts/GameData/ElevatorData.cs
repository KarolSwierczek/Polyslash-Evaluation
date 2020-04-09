namespace KarolSwierczek.Elevator.GameData
{
    using UnityEngine;

    /// <summary>
    /// settings for <see cref="Gameplay.ElevatorController"/>
    /// </summary>
    [CreateAssetMenu(menuName = "Elevator/GameData/ElevatorData")]
    [System.Serializable]

    public sealed class ElevatorData : ScriptableObject
    {
        #region Public Variables
        public float Speed => _Speed;
        #endregion Public Variables

        #region Public Methods
        public float GetFloorHeight(int floor)
        {
            if(floor >= _FloorHeights.Length) { throw new System.ArgumentException("Floor: " + floor + "is not defined!"); }
            return _FloorHeights[floor];
        }
        #endregion Public Methods

        #region Private Variables
        [Tooltip("Table defining the height of each floor ")]
        [SerializeField] private float[] _FloorHeights;

        [Tooltip("Elevator moving speed")]
        [SerializeField] private float _Speed;
        #endregion Private Variables

    }
}
