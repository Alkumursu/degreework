using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    private CharacterController _controller;

    private Vector3 _velocity;
    private bool _groundedPlayer;
    [SerializeField] private float _jumpHeight = 5.0f;
    private bool _jumpPressed = false;
    private float _gravity = -9.81f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovementJump();
    }

    void MovementJump()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer)
        {
            _velocity.y = 0.0f;
        }

        if(_jumpPressed && _groundedPlayer)
        {
            _velocity.y += Mathf.Sqrt(_jumpHeight * -1.0f * _gravity);
            _jumpPressed = false;
        }

        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    void OnJump()
    {
        Debug.Log("Jump pressed!");
        if(_controller.velocity.y == 0)
        {
            Debug.Log("Can jump");
            _jumpPressed = true;
        }
        else
        {
            Debug.Log("Cannot jump");
        }
    }
}
