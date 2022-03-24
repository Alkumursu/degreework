using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private MeshRenderer _meshRend;

    void Start()
    {
        _meshRend = GetComponent<MeshRenderer>();
        if (_meshRend is null)
            Debug.LogError("MeshRenderer is null!");
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MovableCrate"))
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            Debug.Log("Distance: " + distance);
            
            if(distance < 0.5f)
            {
                Rigidbody _rb = other.attachedRigidbody;
                if(!(_rb is null))
                {
                    _rb.isKinematic = true;
                    _meshRend.material.color = Color.red;
                    Destroy(this);
                }
            }
        }
    }
}

