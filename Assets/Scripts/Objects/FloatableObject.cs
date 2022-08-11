using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatableObject : MonoBehaviour
{
    bool InWater; 
        //=> submergence > 0f;
    float submergence;
    Rigidbody _rb;

    [SerializeField] LayerMask waterMask;
    Vector3 upAxis = new Vector3(0, 1, 0);

    [SerializeField] float submergenceOffset = 0.5f;
    [SerializeField, Min(0.1f)] float submergenceRange = 1f;

    //[SerializeField, Min(0f)] float buoyancy = 1f;

    [SerializeField, Range(0f, 10f)] float waterDrag = 1f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        /*if (!Physics.Raycast(_rb.position, -upAxis, out RaycastHit hit, 2.5f, waterMask, QueryTriggerInteraction.Ignore))
        {

        }*/

        if (InWater)
        {
            _rb.drag = waterDrag;
            //_rb.velocity += Physics.gravity * ((1f - buoyancy * submergence) * Time.deltaTime);
            _rb.AddForce(Vector3.up * Mathf.Lerp(0,1500,Time.fixedDeltaTime), ForceMode.Acceleration);
            _rb.mass = 10;
        }
        else
        {
            _rb.drag = 0f;
            _rb.mass = 3f;
        }
    }

    void ClearState()
    {
        submergence = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((waterMask & (1 << other.gameObject.layer)) != 0)
        {
            EvaluateSubmergence();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((waterMask & (1 << other.gameObject.layer)) != 0)
        {
            EvaluateSubmergence();
        }
    }

    private void EvaluateSubmergence()
    {
        if (Physics.Raycast(_rb.position + upAxis * submergenceOffset, -upAxis,
            out RaycastHit hit, submergenceRange + 1f, waterMask, QueryTriggerInteraction.Collide))
        {
            submergence = 1f - hit.distance / submergenceRange;
        }
        else
        {
            submergence = 1f;
        }

        if(submergence >= 1f)
        {
            InWater = true;
        }
        else
        {
            InWater = false;
        }
    }
}
