using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    [SerializeField]
    private Transform _handsPos;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LedgeGrabChecker"))
        {
            if(other.transform.parent.TryGetComponent(out PlayerController player))
            {
                player.GrabLedge(_handsPos.position);
            }
        }
    }
}
