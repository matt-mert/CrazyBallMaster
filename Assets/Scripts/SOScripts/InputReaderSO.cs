using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "ScriptableObjects/Input Reader")]
public class InputReaderSO : ScriptableObject, PlayerControls.IGameplayActions, PlayerControls.IMenuActions, PlayerControls.IDialogueActions
{
    // Gameplay
    
    public event UnityAction<Vector2> MoveEvent = delegate { };
    
    // Menu
    
    public event UnityAction PressEvent = delegate { };
    
    // Dialogue
    
    public event UnityAction ContinueEvent = delegate { };

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
        playerControls.Gameplay.Disable();
        playerControls.Menu.Disable();
        playerControls.Dialogue.Disable();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
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
