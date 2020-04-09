namespace KarolSwierczek.Elevator.Systems 
{ 
    using UnityEngine;
    using Zenject;
    using GameData;

    public sealed class PlayerInteractionInstaller : MonoInstaller
    {
        #region Public Methods
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle().NonLazy();
            Container.BindInstance(_Settings).AsSingle().NonLazy();
        }
        #endregion Public Methods

        #region Inspector Variables
        [SerializeField] private PlayerInteractionData _Settings;
        #endregion Inspector Variables
    }
}