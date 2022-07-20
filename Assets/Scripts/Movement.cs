using UnityEngine;
using NotEnoughCheddar.SOEvents;

public class Movement : MonoBehaviour
{
    [SerializeField] private InputReaderSO inputReader;

    private Vector2 inputVector;
    private Rigidbody playerRb;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        inputReader.MoveEvent += OnMoveAction;
    }

    private void OnDisable()
    {
        inputReader.MoveEvent -= OnMoveAction;
    }

    private void Update()
    {
        
    }

    private void OnMoveAction(Vector2 input)
    {
        inputVector = input;
        Debug.Log(inputVector);
    }
}
