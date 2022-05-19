using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class ManagerInput : MonoBehaviour
{
    public static ManagerInput Instance { get; private set; }
    
    public event Action OnBeginContact;
    public event Action<Vector2> OnDuringContact;
    public event Action OnEndContact;

    private PlayerControls playerControls;

    public static bool isPressing;
    public static bool isPerformed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playerControls = new PlayerControls();
        isPressing = false;
        isPerformed = false;
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChange;

        if (playerControls == null) return;
        playerControls.Enable();
        playerControls.General.Contact.started += ctx => BeginContact(ctx);
        playerControls.General.Move.performed += ctx => DuringContact(ctx);
        playerControls.General.Contact.canceled += ctx => EndContact(ctx);
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;

        if (playerControls == null) return;
        playerControls.General.Contact.started -= BeginContact;
        playerControls.General.Move.performed -= DuringContact;
        playerControls.General.Contact.canceled -= EndContact;
        playerControls.Disable();
    }

    private void OnSceneChange(Scene current, Scene next)
    {
        playerControls = new PlayerControls();
        isPressing = false;
        isPerformed = false;
    }

    private void BeginContact(InputAction.CallbackContext ctx)
    {
        if (OnBeginContact == null) return;
        if (ManagerGame.CurrentState != ManagerGame.GameStates.Playing) return;
        OnBeginContact.Invoke();
        isPressing = true;
    }

    private void DuringContact(InputAction.CallbackContext ctx)
    {
        if (OnDuringContact == null || isPressing == false) return;
        if (ManagerGame.CurrentState != ManagerGame.GameStates.Playing) return;
        OnDuringContact.Invoke(ctx.ReadValue<Vector2>());
        isPerformed = true; // becomes false again elsewhere.
    }

    private void EndContact(InputAction.CallbackContext ctx)
    {
        if (OnEndContact == null) return;
        if (ManagerGame.CurrentState != ManagerGame.GameStates.Playing) return;
        OnEndContact.Invoke();
        isPressing = false;
    }
}
