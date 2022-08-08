using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    bool isActivated = false;
    public PressurePlateManager manager;

    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
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

    public bool IsActivated()
    {
        return isActivated;
    }
}
