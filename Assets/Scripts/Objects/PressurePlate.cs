using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    bool isActivated = false;
    public PressurePlateManager manager;

    private bool crateOnPlate = false;
    private bool madisonOnPlate = false;
    private bool emmaOnPlate = false;

    void Start()
    {
        
    }


    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MovableCrate"))
        {
            isActivated = true;
            manager.CheckAllPlates();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            isActivated = true;
            manager.CheckAllPlates();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MovableCrate"))
        {
            isActivated = false;
            manager.CheckAllPlates();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            isActivated = false;
            manager.CheckAllPlates();
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Emma"))
        {
            emmaOnPlate = true;
            Debug.Log("Emma has stepped on the plate");

            if (madisonOnPlate == false && crateOnPlate == false)
            {
                isActivated = true;
                FindObjectOfType<AudioManager>().Play("PressurePlateSound");
                manager.CheckAllPlates();
            }
        }

        if (other.gameObject.CompareTag("Madison"))
        {
            madisonOnPlate = true;
            Debug.Log("Madison has stepped on the plate");

            if (emmaOnPlate == false && crateOnPlate == false)
            {
                isActivated = true;
                FindObjectOfType<AudioManager>().Play("PressurePlateSound");
                manager.CheckAllPlates();
            }
        }

        if (other.gameObject.CompareTag("MovableCrate"))
        {
            crateOnPlate = true;
            Debug.Log("Crate is on the plate");

            if (madisonOnPlate == false && emmaOnPlate == false)
            {
                isActivated = true;
                FindObjectOfType<AudioManager>().Play("PressurePlateSound");
                manager.CheckAllPlates();
            }
        }
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
            isActivated = false;
            FindObjectOfType<AudioManager>().Play("PressurePlateSound");
            manager.CheckAllPlates();
        }
    }

    public bool IsActivated()
    {
        return isActivated;
    }
}
