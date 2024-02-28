using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsPlayerManager : MonoBehaviour
{
    // Input Manager
    public Movement_Input movementInput;

    // Movement
    public float moveSpeed = 6.0f;
    private CharacterController characterController;

    private void Awake()
    {
        movementInput = new Movement_Input();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandlingPlayerMovement();
    }

    private void HandlingPlayerMovement()
    {
        Vector2 inputVector = movementInput.Player.Move.ReadValue<Vector2>();
        Vector3 moveDirection = (inputVector.y * transform.forward).normalized + (inputVector.x * transform.right).normalized;
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        movementInput.Enable();
    }

    private void OnDisable()
    {
        movementInput.Disable();
    }
}
