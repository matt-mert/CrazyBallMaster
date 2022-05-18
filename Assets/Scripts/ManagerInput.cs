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

    public static bool isPressing = false;
    public static bool isPerformed = false;

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
    }

    private void BeginContact()
    {
        if (OnBeginContact == null) return;
        OnBeginContact.Invoke();
        isPressing = true;
        
        // if state is menu:
        // InitializeGame();
        // OnBeginContract.Invoke();
        // isPressing = true;
    }

    private void DuringContact(InputAction.CallbackContext ctx)
    {
        if (OnDuringContact == null || isPressing == false) return;
        OnDuringContact.Invoke(ctx.ReadValue<Vector2>());
        isPerformed = true;
    }

    private void EndContact()
    {
        if (OnEndContact == null) return;
        OnEndContact.Invoke();
        isPressing = false;
    }
}
