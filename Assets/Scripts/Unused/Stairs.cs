using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    [SerializeField] GameObject player;
    public GameObject TeleportTo1, TeleportTo2;


    void Start()
    {
 
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter (Collider collision)
    {
        if (collision.gameObject.CompareTag("StairTeleporter1"))
        {
            player.transform.position = TeleportTo1.transform.position;
        }

        if (collision.gameObject.CompareTag("StairTeleporter2"))
        {
            player.transform.position = TeleportTo2.transform.position;
        }
    }
}
