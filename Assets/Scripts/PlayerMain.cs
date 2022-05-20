using System;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    [SerializeField] private InputReaderSO inputReader;

    private Vector2 inputVector;
    [SerializeField] private float playerSpeed;
    [NonSerialized] public Vector3 movementVector;

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
        Vector3 input = new(inputVector.x, inputVector.y, 0);
        movementVector = input * playerSpeed * Time.deltaTime;
    }

    private void OnMoveAction(Vector2 input)
    {
        inputVector = input;
    }
}
