using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace NotEnoughCheddar.SOEvents
{
    [CreateAssetMenu(fileName = "NewInputReader", menuName = "ScriptableObjects/Input Reader")]
    public class InputReaderSO : ScriptableObject, PlayerControls.IGameplayActions,
        PlayerControls.IMenuActions, PlayerControls.IDialogueActions
    {
        // Gameplay Events
        
        public event UnityAction<Vector2> MoveEvent = delegate {  };
        
        // Menu Events
        
        public event UnityAction PressEvent = delegate {  };
        
        // Dialogue Events
        
        public event UnityAction ContinueEvent = delegate {  };
    
        private PlayerControls playerControls;
    
        private void OnEnable()
        {
            if (playerControls == null) return;
            playerControls = new PlayerControls();
            playerControls.Gameplay.SetCallbacks(this);
            playerControls.Menu.SetCallbacks(this);
            playerControls.Dialogue.SetCallbacks(this);
        }
    
        private void OnDisable()
        {
            if (playerControls == null) return;
            playerControls.Gameplay.Disable();
            playerControls.Menu.Disable();
            playerControls.Dialogue.Disable();
        }
    
        public void OnMove(InputAction.CallbackContext ctx)
        {
            if (ctx.phase == InputActionPhase.Performed)
            {
                Debug.Log("Performed");
                MoveEvent.Invoke(ctx.ReadValue<Vector2>());
            }
        }
    
        public void OnPress(InputAction.CallbackContext ctx)
        {
            if (ctx.phase == InputActionPhase.Performed)
            {
                PressEvent.Invoke();
            }
        }
    
        public void OnContinue(InputAction.CallbackContext ctx)
        {
            if (ctx.phase == InputActionPhase.Performed)
            {
                ContinueEvent.Invoke();
            }
        }
    }
}
