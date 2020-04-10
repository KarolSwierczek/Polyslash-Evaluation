namespace KarolSwierczek.Elevator.Gameplay
{
    using Systems;
    using UnityEngine;
    using Zenject;
    using Sirenix.OdinInspector;

    /// <summary>
    /// this class is attached to every elevator button and responsible for calling the elevator to specified floor
    /// </summary>
    public sealed class ButtonComponent : MonoBehaviour, IInteractable
    {
        #region Inspector Variables
        [FoldoutGroup("Settings"), BoxGroup("Settings/General"), Tooltip("Target floor number ")]
        [SerializeField] private int _Floor;

        [FoldoutGroup("Settings"), BoxGroup("Settings/General"), Tooltip("UI message that appears when player looks at this button ")]
        [SerializeField] private string _Message;

        [FoldoutGroup("Settings"), BoxGroup("Settings/Audio"), Tooltip("Clip that plays on successful button press ")]
        [SerializeField] private AudioClip _PressSuccess;

        [FoldoutGroup("Settings"), BoxGroup("Settings/Audio"), Tooltip("Clip that plays on rejected button press ")]
        [SerializeField] private AudioClip _PressFail;

        [FoldoutGroup("References")]
        [SerializeField] private AudioSource _AudioSource;
        #endregion Inspector Variables

        #region Private Variables
        [Inject] private readonly ElevatorController _Controller;
        [Inject] private readonly UIController _UI;
        #endregion Private Variables

        #region Private Methods
        [Button]
        private void OnButtonPressed(int floor)
        {
            var success = _Controller.TryRequestFloor(floor);
            _AudioSource.clip = success ? _PressSuccess : _PressFail;
            _AudioSource.Play();
        }

        void IInteractable.OnUse()
        {
            OnButtonPressed(_Floor);
        }

        void IInteractable.OnVisible()
        {
            _UI.ShowMessage(_Message);
        }

        void IInteractable.OnInvisible()
        {
            _UI.HideMessage();
        }
        #endregion Private Methods
    }
}
