using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField]
    private float _movementSpeed;
    //private PlayerActions _playerActions;
    private Rigidbody _rb;
    private Vector2 _moveInput;

    //[SerializeField]
    //private float _jumpSpeed;
    //private LayerMask _groundMask;
    //private BoxCollider _collider;
    //private bool _jumping;

    //public float gravityScale = 1.0f;
    //public static float globalGravity = -9.81f;

    [SerializeField]
    private float _pushPower;

    private void Awake()
    {
        //_playerActions = new PlayerActions();
        _controller = GetComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();
        if (_rb is null)
            Debug.LogError("Rigidbody is null!");
    }

    private void Update()
    {
        Moving();
        //OnJump();
    }

    private void OnEnable()
    {
        //_playerActions.Player_Map.Enable();

        //_rb.useGravity = false;
    }

    private void OnDisable()
    {
        //_playerActions.Player_Map.Disable();
    }

    private void FixedUpdate()
    {
        //_moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        //_moveInput.y = 0f;
        //_rb.velocity = _moveInput * _movementSpeed;

        //Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        //_rb.AddForce(gravity, ForceMode.Acceleration);
    }

    /*
    void OnJump(InputValue value)
    {
        //if(!isAlive) {return;}

        if (value.isPressed)
        {
            //_jumping = true;
            _rb.AddForce(Vector3.up * _jumpSpeed, ForceMode.Impulse);
        }
    }
    */
    void OnMovement(InputValue value)
    {
        Debug.Log("Moving!");
        _moveInput = value.Get<Vector2>();
    }

    void Moving()
    {
        Vector2 movement = new Vector2(_moveInput.x, 0.0f);
        _controller.SimpleMove(movement * _movementSpeed);
    }




    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("MovableCrate"))
        {
            Rigidbody box = hit.collider.attachedRigidbody;

            if(!(box is null))
            {
                Vector3 moveDir = new Vector3(hit.moveDirection.x, 0, 0);
                box.velocity = moveDir * _pushPower;
            }
        }
    }



}