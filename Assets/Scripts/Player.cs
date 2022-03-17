using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    private PlayerActions playerActions;
    private Rigidbody rb;
    private Vector2 moveInput;

    [SerializeField]
    private float _jumpSpeed;
    private LayerMask _groundMask;
    private BoxCollider _collider;
    private bool _jumping;

    public float gravityScale = 1.0f;
    public static float globalGravity = -9.81f; 

    /*[SerializeField]
    private float _jumpPower;
    [SerializeField]
    [Range(1f, 5f)]
    private float _jumpFallGravityMultiplier;

    private BoxCollider _collider;

    [SerializeField]
    private float _groundCheckHeight;
    [SerializeField]
    private LayerMask _groundMask;
    [SerializeField]
    private float _disableGCTime;

    private Vector3 _boxCenter;
    private Vector3 _boxSize;
    private bool _jumping;
    private float _initialGravityScale;
    private bool _groundCheckEnabled = true;
    private WaitForSeconds _wait;
    */

    private void Awake()
    {
        playerActions = new PlayerActions();

        rb = GetComponent<Rigidbody>();
        if (rb is null)
            Debug.LogError("Rigidbody is null!");
    }

    private void Update()
    {
        //OnJump();
    }

    private void OnEnable()
    {
        playerActions.Player_Map.Enable();

        rb.useGravity = false;
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

        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    void OnJump(InputValue value)
    {
        //if(!isAlive) {return;}
        //if(!_collider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (value.isPressed)
        {
            _jumping = true;
            Vector2 jumpVelocityToAdd = new Vector2(0f, _jumpSpeed);
            rb.AddForce(Vector3.up * _jumpSpeed, ForceMode.Impulse);
        }
    }
}