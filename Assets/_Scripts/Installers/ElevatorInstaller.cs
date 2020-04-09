namespace KarolSwierczek.Elevator.Gameplay {

    using GameData;
    using UnityEngine;
    using Zenject;

    public sealed class ElevatorInstaller : MonoInstaller
    {
        #region Public Methods
        public override void InstallBindings()
        {
            Container.BindInstance(_Settings).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ElevatorController>().AsSingle().NonLazy();
        }
        #endregion Public Methods

        #region Inspector Variables
        [SerializeField] private ElevatorData _Settings;
        #endregion Inspector Variables

    }
}
