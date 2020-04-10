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

        #region Private Variables
        [Tooltip("Elevator moving speed")]
        [SerializeField] private float _Speed;
        #endregion Private Variables

    }
}
