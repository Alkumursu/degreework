using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Player movement
    [SerializeField]
    private float _speed;
    private PlayerActions _playerActions;
    private Rigidbody _rb;
    private Vector2 _moveInput;

    //Jumping
    [SerializeField]
    private float _jumpPower;
    private bool _grounded;

    [SerializeField]
    private float _pushPower;


    void Awake()
    {
        _playerActions = new PlayerActions();

        _rb = GetComponent<Rigidbody>();
        if (_rb is null)
            Debug.LogError("Rigidbody is null!");
    }

    void Start()
    {
        _playerActions.Player_Map.Jump.performed += _ => HandleJump();
    }

    private void OnEnable()
    {
        _playerActions.Player_Map.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Player_Map.Disable();
    }


    void FixedUpdate()
    {
        HandleMovement();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Physics.Raycast(transform.position, Vector3.down, 2f))
        {
            _grounded = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Physics.Raycast(transform.position, Vector3.down, 2f))
        {
            _grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _grounded = false;
    }

    private void HandleMovement()
    {
        _moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        _moveInput.y = _rb.velocity.y;
        Vector3 movementVelocity = new Vector3(_moveInput.x * _speed, _rb.velocity.y, 0);
        _rb.velocity = Vector3.Lerp(_rb.velocity, movementVelocity, Time.fixedDeltaTime);
    }

    private void HandleJump()
    {
        if (_grounded)
        {
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("MovableCrate"))
        {
            Rigidbody box = hit.collider.attachedRigidbody;

            if (!(box is null))
            {
                Vector3 moveDir = new Vector3(hit.moveDirection.x, 0, 0);
                box.velocity = moveDir * _pushPower;
            }
        }
    }
}

