using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadisonSubmergence : MonoBehaviour
{
    ControllableCharacter cc;
    Rigidbody _rb;
    //Vector3 upAxis = Vector3.up;
    Vector3 upAxis = new Vector3(0, 1, 0);

    [SerializeField] float submergenceOffset = 0.5f;
    [SerializeField, Min(0.1f)]
    float submergenceRange = 1f;
    [SerializeField, Range(0f, 10f)] float waterDrag = 1f;

    bool InWater; 
    //=> submergence > 0f;
    float submergence;

    [SerializeField] LayerMask waterMask;

    //Floating
    //float buoyancy = 1f;

    void Start()
    {
        cc = GetComponent<ControllableCharacter>();
        _rb = GetComponent<Rigidbody>();
    }

    void ClearState()
    {
        submergence = 0f;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (InWater)
        {
            _rb.drag = waterDrag;
            //_rb.velocity += Physics.gravity * ((1f - buoyancy * submergence) * Time.deltaTime);
            _rb.AddForce(Vector3.up * Mathf.Lerp(0, 600, Time.fixedDeltaTime), ForceMode.Acceleration);
        }
        else
        {
            _rb.drag = 0f;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if ((waterMask & (1 << other.gameObject.layer)) != 0)
        {
            EvaluateSubmergence();
            Debug.Log("Water");
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
        if (Physics.Raycast(_rb.position + upAxis * submergenceOffset,
            -upAxis, out RaycastHit hit, submergenceRange + 1f, waterMask, QueryTriggerInteraction.Collide))
        {
            submergence = 1f - hit.distance / submergenceRange;

            if (submergence > 0.88f)
                //aiemmin 0.66f
            {
                Debug.Log("Madison death sequence has started");
                cc.MadisonDeath();
            }
        }
        if (submergence >= 0.90f)
        {
            InWater = true;
            cc.MadisonDeath();
        }
        else
        {
            InWater = false;
        }
    }

}
