using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    private PlayerActions playerActions;
    private Rigidbody rb;
    private Vector2 moveInput;

    private void Awake()
    {
        playerActions = new PlayerActions();

        rb = GetComponent<Rigidbody>();
        if (rb is null)
            Debug.LogError("Rigidbody is null!");
    }

    private void OnEnable()
    {
        playerActions.Player_Map.Enable();
    }

    private void OnDisable()
    {
        playerActions.Player_Map.Disable();
    }

    private void FixedUpdate()
    {
        moveInput = playerActions.Player_Map.Movement.ReadValue<Vector2>();
        moveInput.y = 0f;
        rb.velocity = moveInput * movementSpeed;
    }
}