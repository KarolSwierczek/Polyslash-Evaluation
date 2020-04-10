namespace KarolSwierczek.Elevator.Gameplay {

    using GameData;
    using UnityEngine;
    using Zenject;

    public sealed class ElevatorInstaller : MonoInstaller
    {
        #region Public Methods
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ElevatorController>().AsSingle().WithArguments(_Settings, _FloorContainer).NonLazy();
        }
        #endregion Public Methods

        #region Inspector Variables
        [SerializeField] private ElevatorData _Settings;
        [SerializeField] private FloorContainter _FloorContainer;
        #endregion Inspector Variables

    }
}
