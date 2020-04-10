namespace KarolSwierczek.Elevator.Systems
{
    using System.Collections.Generic;
    using UnityEngine;
    using GameData;
    using Zenject;
    using Sirenix.OdinInspector;

    /// <summary>
    /// this class handles players interactions with objects that implement <see cref="IInteractable"/> interface
    /// </summary>
    public sealed class PlayerInteractionController : MonoBehaviour
    {
        #region Public Methods
        public void Interact() 
        {
            _CurrentVisibleItems.ForEach(x => x.OnUse());
        }
        #endregion Public Methods

        #region Unity Methods
        private void OnEnable()
        {
            _InputHandler.OnPlayerInteract += OnPlayerInteract;
        }

        private void OnDisable()
        {
            _InputHandler.OnPlayerInteract -= OnPlayerInteract;
        }

        private void Update()
        {
            /*if there's anything in player interaction range */
            if (Physics.Raycast(_Camera.position, _Camera.forward, out RaycastHit hit, _Data.Range, _Data.InteractionMask))
            {
                /* ...and it's an interactable object */
                var interactables = hit.transform.GetComponents<IInteractable>();

                foreach(var interactable in interactables)
                {
                    if (!_CurrentVisibleItems.Contains(interactable))
                    {
                        _CurrentVisibleItems.Add(interactable);
                        interactable.OnVisible();
                    }
                }
                
                if(interactables.Length > 0) { return; }
            }
            /* if there are no interactable object in sight */
            if(_CurrentVisibleItems.Count > 0)
            {
                _CurrentVisibleItems.ForEach(x => x.OnInvisible());
                _CurrentVisibleItems.Clear();
            }
        }
        #endregion Unity Methods

        #region Inspector Variables
        [FoldoutGroup("References")]
        [SerializeField] private Transform _Camera;
        #endregion Inspector Variables

        #region Private Variables
        [Inject] private PlayerInteractionData _Data;
        [Inject] private InputHandler _InputHandler;

        private readonly List<IInteractable> _CurrentVisibleItems = new List<IInteractable>();
        #endregion Private Variables

        #region Private Methods
        private void OnPlayerInteract(object sender, InputHandler.OnPlayerInteractArgs args)
        {
            Interact();
        }
        #endregion Private Methods
    }
}
