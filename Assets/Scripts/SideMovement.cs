using UnityEngine;

public class SideMovement : MonoBehaviour
{
    private Vector2 inputVector;
    private Vector3 moveVector;

    [SerializeField] private float sensitivity;

    private void OnEnable()
    {
        ManagerInput.Instance.onBeginContact += BeginMovement;
        ManagerInput.Instance.onDuringContact += DuringMovement;
        ManagerInput.Instance.onEndContact += EndMovement;
    }

    private void OnDisable()
    {
        if (gameObject.scene.isLoaded) return;
        if (ManagerInput.Instance == null) return;
        ManagerInput.Instance.onBeginContact -= BeginMovement;
        ManagerInput.Instance.onDuringContact -= DuringMovement;
        ManagerInput.Instance.onEndContact -= EndMovement;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void FixedUpdate()
    {
        if (ManagerInput.Instance.isPerformed == false) return;
        moveVector = new Vector3(inputVector.normalized.x * sensitivity * Time.fixedDeltaTime, 0, 0);
        transform.localPosition += moveVector;
        moveVector = Vector3.zero;
        ManagerInput.Instance.isPerformed = false;
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
