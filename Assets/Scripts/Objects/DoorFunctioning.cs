using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFunctioning : MonoBehaviour
{
    /*
    private MeshRenderer _meshRend;
    // Door opening function
    [SerializeField] GameObject door1, door2;
    bool isOpened;
    */

    //[SerializeField] GameObject door;
    Animator _anim;

    void Start()
    {
        /*_meshRend = GetComponent<MeshRenderer>();
        if (_meshRend is null)
            Debug.LogError("MeshRenderer is null!");
        */

        _anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("MovableCrate"))
        {
            _anim.SetTrigger("DoorTrigger");
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            _anim.SetTrigger("DoorTrigger");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MovableCrate"))
        {
            _anim.SetTrigger("DoorTrigger");
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            _anim.SetTrigger("DoorTrigger");
        }
    }


    /*
    // FOR THE OFF COMMENTED STUFF TO WORK, THE FOLLOWING NEEDS TO BE IN THE PRESSURE PLATE INSTEAD
    // Easy way, changing doors' positions 
    private void OnTriggerEnter(Collider col)
    {
        if (!isOpened)
        {
            isOpened = true;
            door1.transform.position += new Vector3(0, 0, -1.1f);
            door2.transform.position += new Vector3(0, 0, 1.1f);
        }
    }

    // In case we want to have one-time-use crates
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
    */
}

