namespace KarolSwierczek.Elevator.Systems
{
    /// <summary>
    /// base interface for any interactable object
    /// </summary>
    public interface IInteractable
    {
        #region Methods
        /// <summary>
        /// invoked when a player interacted with this object
        /// </summary>
        void OnUse();

        /// <summary>
        /// invoked when this object comes into players focus
        /// </summary>
        void OnVisible();

        /// <summary>
        /// invoked when this object comes out of players focus
        /// </summary>
        void OnInvisible();
        #endregion Methods
    }
}
