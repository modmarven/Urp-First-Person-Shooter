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

    // Mouse Look 
    [Header("MouseLook")]
    public Transform cameraPosition;
    private float xRotation;
    private float yRotation;
    public float limitRotation = 60f;
    public float mouseSensetive = 100f;

    // Gravity
    [Header("GravityMultiply")]
    public float gravity = -9.81f;
    private Vector3 velocity;

    private void Awake()
    {
        movementInput = new Movement_Input();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandlingPlayerMovement();
        HandlingMouseLook();
    }

    private void HandlingPlayerMovement()
    {
        Vector2 inputVector = movementInput.Player.Move.ReadValue<Vector2>();
        Vector3 moveDirection = (inputVector.y * transform.forward).normalized + (inputVector.x * transform.right).normalized;
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Gravity Apply
        if (characterController.isGrounded)
        {
            velocity.y = 0;
        }

        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    private void HandlingMouseLook()
    {
        Vector2 lookVector = movementInput.Player.Look.ReadValue<Vector2>();
        float mouseX = lookVector.x;
        float mouseY = lookVector.y;
        yRotation += mouseX * mouseSensetive * Time.deltaTime;
        xRotation -= mouseY * mouseSensetive * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -limitRotation, limitRotation);

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        cameraPosition.rotation = Quaternion.Euler(xRotation, yRotation, 0);
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
