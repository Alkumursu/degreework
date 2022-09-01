using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PressurePlateDoorFunctioning : MonoBehaviour
{
    /*
    private MeshRenderer _meshRend;
    // Door opening function
    [SerializeField] GameObject door1, door2;
    bool isOpened;
    */

    //[SerializeField] GameObject door;
    Animator _anim;

    private bool crateOnPlate = false;
    private bool madisonOnPlate = false;
    private bool emmaOnPlate = false;
    //private bool plateIsPressured = false;

    [SerializeField] Light doorLight, plateLight;
    private bool lightsOn = false;

    void Start()
    {
        /*_meshRend = GetComponent<MeshRenderer>();
        if (_meshRend is null)
            Debug.LogError("MeshRenderer is null!");
        */
        _anim = this.GetComponent<Animator>();

        doorLight.DOIntensity(0, 0.2f);
        plateLight.DOIntensity(0, 0.2f);
    }

    void Update()
    {
        if (lightsOn == true)
        {
            doorLight.DOIntensity(1, 0.2f);
            plateLight.DOIntensity(1, 0.2f);
        }
        else
        {
            doorLight.DOIntensity(0, 0.2f);
            plateLight.DOIntensity(0, 0.2f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Emma"))
        {
            emmaOnPlate = true;
            lightsOn = true;
            doorLight.DOIntensity(1, 0.2f);
            plateLight.DOIntensity(1, 0.2f);
            Debug.Log("Emma has stepped on the plate");

            if (madisonOnPlate == false && crateOnPlate == false)
            {
                _anim.Play("DoorSlideOpen");
            }
        }

        if (other.gameObject.CompareTag("Madison"))
        {
            madisonOnPlate = true;
            lightsOn = true;
            Debug.Log("Madison has stepped on the plate");
                
            if (emmaOnPlate == false && crateOnPlate == false)
            {
                 _anim.Play("DoorSlideOpen");
            }
        }

        if (other.gameObject.CompareTag("MovableCrate"))
        {
            crateOnPlate = true;
            lightsOn = true;
            Debug.Log("Crate is on the plate");

            if (madisonOnPlate == false && emmaOnPlate == false)
            {
                _anim.Play("DoorSlideOpen");
            }
        }
           
            /*if (other.gameObject.CompareTag("Madison") || other.gameObject.CompareTag("Emma")
                || other.gameObject.CompareTag("MovableCrate"))
            {
                //_anim.SetTrigger("DoorTrigger");
                //crateOnPlate = true;

                //_anim.SetBool("DoorSlideOpen", true);
                //_anim.SetBool("DoorSlideClose", false);
                _anim.Play("DoorSlideOpen");
                plateIsPressured = true;
                //madisonOnPlate = true;
                Debug.Log("Something on the plate");
            }
            */
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Madison"))
        {
            madisonOnPlate = false;
            Debug.Log("Madison has stepped off the plate");
        }

        if (other.gameObject.CompareTag("Emma"))
        {
            emmaOnPlate = false;
            Debug.Log("Emma has stepped off the plate");
        }

        if (other.gameObject.CompareTag("MovableCrate"))
        {
            crateOnPlate = false;
            Debug.Log("Crate is off the plate");
        }

        if (emmaOnPlate == false && madisonOnPlate == false && crateOnPlate == false)
        {
            Debug.Log("Door should close");
            lightsOn = false;
            doorLight.DOIntensity(0, 0.2f);
            plateLight.DOIntensity(0, 0.2f);
            _anim.Play("DoorSlideClose");
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

