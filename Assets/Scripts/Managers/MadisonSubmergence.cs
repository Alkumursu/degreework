using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadisonSubmergence : MonoBehaviour
{
    ControllableCharacter cc;
    Rigidbody _rb;
    Vector3 upAxis = Vector3.up;

    [SerializeField] float submergenceOffset = 0.5f;
    [SerializeField, Min(0.1f)]
    float submergenceRange = 1f;

    bool InWater => submergence > 0f;
    float submergence;
    [SerializeField] LayerMask waterMask;

    void Start()
    {
        cc = GetComponent<ControllableCharacter>();
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //Swimming
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
            if (submergence > 0.9f)
                //aiemmin 0.66f
            {
                cc.MadisonDeath();
            }
        }
    }
}
