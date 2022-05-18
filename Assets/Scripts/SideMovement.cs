using UnityEngine;

public class SideMovement : MonoBehaviour
{
    private ManagerInput managerInput;
    private Vector2 inputVector;
    private Vector3 moveVector;

    [SerializeField] private float roadWidth;
    [SerializeField] private float sensitivity;

    private void OnEnable()
    {
        managerInput.OnBeginContact += BeginMovement;
        managerInput.OnDuringContact += DuringMovement;
        managerInput.OnEndContact += EndMovement;
    }

    private void OnDisable()
    {
        managerInput.OnBeginContact -= BeginMovement;
        managerInput.OnDuringContact -= DuringMovement;
        managerInput.OnEndContact -= EndMovement;
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
        if (transform.localPosition.x > roadWidth / 2 && inputVector.x > 0)
        {
            inputVector = Vector2.zero;
        }
        else if (transform.localPosition.x < -roadWidth / 2 && inputVector.x < 0)
        {
            inputVector = Vector2.zero;
        }

        moveVector = new Vector3(inputVector.normalized.x * sensitivity * Time.fixedDeltaTime, 0, 0);

        if (ManagerInput.isPerformed == false) return;

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
