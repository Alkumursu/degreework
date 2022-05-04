using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField]
    private float _movementSpeed;
    private Rigidbody _rb;
    private Vector2 _moveInput;
    private Vector3 _velocity;
    //private float _gravity = -9.81f;

    [SerializeField]
    private float _pushPower;

    //In-Water behavior
    [SerializeField] 
    LayerMask waterMask = 0;
    [SerializeField]
    Material
        normalMaterial = default,
        swimmingMaterial = default;

    MeshRenderer meshRenderer;

    bool InWater;
    float submergence;

    //[SerializeField, Range(0f, 10f)]
    //float waterDrag = 1f;

    //Defines when the player counts as being in water and when it's fully submerged
    [SerializeField]
    float submergenceOffset = 0.5f;
    [SerializeField, Min(0.1f)]
    float submergenceRange = 1f;

    Vector3 upAxis = new Vector3(0,1,0);


    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        _controller = GetComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();
        if (_rb is null)
            Debug.LogError("Rigidbody is null!");
    }

    void ClearState()
    {
        submergence = 0f;
    }


    private void Update()
    {
        Moving();
        meshRenderer.material = InWater ? swimmingMaterial : normalMaterial;
        
        //for testing purposes
        //meshRenderer.material.color = Color.white * submergence;
    }

    /*void FixedUpdate()
    {
        if (InWater)
        {
            //_velocity *= 1f - waterDrag * submergence * Time.deltaTime;
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
        if (InWater)
        {
            Vector2 movement = new Vector2(_moveInput.x, 0.0f);
            _controller.Move(movement * Time.deltaTime * (_movementSpeed * 0.5f));
        }
        else
        {
            Vector2 movement = new Vector2(_moveInput.x, 0.0f);
            _controller.Move(movement * Time.deltaTime * _movementSpeed);
        }
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

    void OnTriggerEnter(Collider other)
    {
        if ((waterMask & (1 << other.gameObject.layer)) != 0)
        {
            EvaluateSubmergence();
        } 
    }

    void OnTriggerStay (Collider other)
    {
        if ((waterMask & (1 << other.gameObject.layer)) != 0)
        {
            EvaluateSubmergence();  
        }
    }

    void EvaluateSubmergence()
    {
        if (Physics.Raycast(
            _rb.position + upAxis * submergenceOffset,
            -upAxis, out RaycastHit hit, submergenceRange + 1f,
            waterMask, QueryTriggerInteraction.Collide
            ))
            {
            submergence = 1f - hit.distance / submergenceRange;
            }
        else
        {
            submergence = 1f;
        }

        if (submergence > submergenceOffset)
        {
            InWater = true;
        }
        else
        {
            InWater = false;
        }
    }
}