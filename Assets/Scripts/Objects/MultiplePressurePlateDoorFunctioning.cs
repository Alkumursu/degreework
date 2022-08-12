using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplePressurePlateDoorFunctioning : MonoBehaviour
{
    Animator _anim;
    Collider col1, col2, col3;

    bool Plate1 = false;
    bool Plate2 = false;
    bool Plate3 = false;
    
    void Start()
    {
        _anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (Plate1 == true && Plate2 == true && Plate3 == true)
        {
            _anim.SetTrigger("DoorTrigger");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (col1.gameObject.CompareTag("MovableCrate"))
        {
            Plate1 = true;
            Debug.Log("Plate 1 active");
        }
        else if (col1.gameObject.CompareTag("Player"))
        {
            Plate1 = true;
            Debug.Log("Plate 1 active");
        }

        if (col2.gameObject.CompareTag("MovableCrate"))
        {
            Plate2 = true;
            Debug.Log("Plate 2 active");
        }
        else if (col2.gameObject.CompareTag("Player"))
        {
            Plate2 = true;
            Debug.Log("Plate 2 active");
        }

        if (col3.gameObject.CompareTag("MovableCrate"))
        {
            Plate3 = true;
            Debug.Log("Plate 3 active");
        }
        else if (col3.gameObject.CompareTag("Player"))
        {
            Plate3 = true;
            Debug.Log("Plate 3 active");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (col1.gameObject.CompareTag("MovableCrate"))
        {
            Plate1 = false;
            Debug.Log("Plate 1 inactive");
        }
        else if (col1.gameObject.CompareTag("Player"))
        {
            Plate1 = false;
            Debug.Log("Plate 1 inactive");
        }

        if (col2.gameObject.CompareTag("MovableCrate"))
        {
            Plate2 = false;
            Debug.Log("Plate 2 inactive");
        }
        else if (col2.gameObject.CompareTag("Player"))
        {
            Plate2 = false;
            Debug.Log("Plate 2 inactive");
        }

        if (col3.gameObject.CompareTag("MovableCrate"))
        {
            Plate3 = false;
            Debug.Log("Plate 3 inactive");
        }
        else if (col3.gameObject.CompareTag("Player"))
        {
            Plate3 = false;
            Debug.Log("Plate 3 inactive");
        }
    }


}
