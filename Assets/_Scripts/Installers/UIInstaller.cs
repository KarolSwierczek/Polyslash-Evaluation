namespace KarolSwierczek.Elevator.Systems
{
    using Sirenix.OdinInspector;
    using UnityEngine;
    using Zenject;

    public class UIInstaller : MonoInstaller
    {
        #region Public Methods
        public override void InstallBindings()
        {
            Container.BindInstance(_Controller).AsSingle().NonLazy();
        }
        #endregion Public Methods

        #region Inspector Variables
        [FoldoutGroup("References")]
        [SerializeField] private UIController _Controller;
        #endregion Inspector Variables
    }
}