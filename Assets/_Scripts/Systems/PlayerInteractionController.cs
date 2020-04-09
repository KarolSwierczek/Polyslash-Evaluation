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
            if(_CurrentVisibleItems.Count == 0) { return; }
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
            if (Physics.Raycast(_Camera.position, _Camera.forward, out _Hit, _Data.Range))
            {
                /* ...and it's an interactable object */
                var interactables = _Hit.transform.GetComponents<IInteractable>();
                if(interactables.Length > 0)
                {
                    foreach(var interactable in interactables)
                    {
                        if (!_CurrentVisibleItems.Contains(interactable))
                        {
                            _CurrentVisibleItems.Add(interactable);
                            interactable.OnVisible();
                        }
                    }

                    return;
                }
            }
            /* if there are no interactable object in shight */
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

        private RaycastHit _Hit = new RaycastHit();
        private List<IInteractable> _CurrentVisibleItems = new List<IInteractable>();
        #endregion Private Variables

        #region Private Methods
        private void OnPlayerInteract(object sender, InputHandler.OnPlayerInteractArgs args)
        {
            Interact();
        }
        #endregion Private Methods
    }
}
