using UnityEngine;

public class SideMovement : MonoBehaviour
{
    private ManagerInput managerInput;
    private Vector2 inputVector;
    private Vector3 moveVector;

    [SerializeField] private float sensitivity;

    private void OnEnable()
    {
        managerInput.onBeginContact += BeginMovement;
        managerInput.onDuringContact += DuringMovement;
        managerInput.onEndContact += EndMovement;
    }

    private void OnDisable()
    {
        managerInput.onBeginContact -= BeginMovement;
        managerInput.onDuringContact -= DuringMovement;
        managerInput.onEndContact -= EndMovement;
    }

    private void Awake()
    {
        managerInput = FindObjectOfType<ManagerInput>();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void FixedUpdate()
    {
        if (ManagerInput.isPerformed == false) return;
        moveVector = new Vector3(inputVector.normalized.x * sensitivity * Time.fixedDeltaTime, 0, 0);
        transform.localPosition += moveVector;
        moveVector = Vector3.zero;
        ManagerInput.isPerformed = false;
    }

    private void BeginMovement()
    {
        inputVector = Vector2.zero;
    }

    private void DuringMovement(Vector2 input)
    {
        inputVector = input;
    }

    private void EndMovement()
    {
        inputVector = Vector2.zero;
    }
}
