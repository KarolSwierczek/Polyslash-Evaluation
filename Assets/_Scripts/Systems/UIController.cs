namespace KarolSwierczek.Elevator.Systems
{
    using UnityEngine;
    using TMPro;
    using Sirenix.OdinInspector;

    /// <summary>
    /// this class is responsible for showing messages on player's overlay
    /// </summary>
    public sealed class UIController : MonoBehaviour
    {
        #region Public Methods
        public void ShowMessage(string message)
        {
            _MessageBoxText.SetText(message);
            _MessageBox.SetActive(true);
        }

        public void HideMessage()
        {
            _MessageBox.SetActive(false);
        }
        #endregion Public Methods

        #region Inspector Variables
        [FoldoutGroup("References")]
        [SerializeField] private TextMeshProUGUI _MessageBoxText;

        [FoldoutGroup("References")]
        [SerializeField] private GameObject _MessageBox;
        #endregion Inspector Variables
    }
}