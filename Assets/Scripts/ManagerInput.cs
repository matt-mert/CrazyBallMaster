using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class ManagerInput : MonoBehaviour
{
    public static ManagerInput Instance { get; private set; }

    public delegate void OnBeginContact();
    public event OnBeginContact onBeginContact;
    public delegate void OnDuringContact(Vector2 ctx);
    public event OnDuringContact onDuringContact;
    public delegate void OnEndContact();
    public event OnEndContact onEndContact;

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
        playerControls.General.Contact.started += ctx => BeginContact();
        playerControls.General.Move.performed += ctx => DuringContact(ctx);
        playerControls.General.Contact.canceled += ctx => EndContact();
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;

        if (playerControls == null) return;
        playerControls.General.Contact.started -= ctx => BeginContact();
        playerControls.General.Move.performed -= ctx => DuringContact(ctx);
        playerControls.General.Contact.canceled -= ctx => EndContact();
        playerControls.Disable();
    }

    private void OnSceneChange(Scene current, Scene next)
    {
        playerControls = new PlayerControls();
        isPressing = false;
        isPerformed = false;
    }

    private void BeginContact()
    {
        if (onBeginContact == null) return;
        if (ManagerGame.CurrentState != ManagerGame.GameStates.Playing) return;
        onBeginContact.Invoke();
        isPressing = true;
    }

    private void DuringContact(InputAction.CallbackContext ctx)
    {
        if (onDuringContact == null || isPressing == false) return;
        if (ManagerGame.CurrentState != ManagerGame.GameStates.Playing) return;
        onDuringContact.Invoke(ctx.ReadValue<Vector2>());
        isPerformed = true; // becomes false again elsewhere.
    }

    private void EndContact()
    {
        if (onEndContact == null) return;
        if (ManagerGame.CurrentState != ManagerGame.GameStates.Playing) return;
        onEndContact.Invoke();
        isPressing = false;
    }
}
