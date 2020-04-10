namespace KarolSwierczek.Elevator.GameData
{
    using UnityEngine;

    /// <summary>
    /// settings for <see cref="Systems.PlayerInteractionController"/>
    /// </summary>
    [CreateAssetMenu(menuName = "Elevator/GameData/PlayerInteractionData")]
    [System.Serializable]

    public sealed class PlayerInteractionData : ScriptableObject
    {
        #region Public Variables
        public float Range => _Range;
        public int InteractionMask => _InteractionMask;
        #endregion Public Variables

        #region Private Variables
        [Tooltip("Player interaction range ")]
        [SerializeField] private float _Range;

        [Tooltip("Layermask for player interaction ")]
        [SerializeField] private LayerMask _InteractionMask;
        #endregion Private Variables

    }
}
