using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSystem : MonoBehaviour
{
    public GameObject flag; //drag and drop the component to the object with the script
    Vector3 SpawnPoint;

    void Start()
    {
        SpawnPoint = gameObject.transform.position;
    }
    void Update()
    {
        if(gameObject.transform.position.y < -20f) //tilalle ehto: jos madison kuolee
        {
            gameObject.transform.position = SpawnPoint;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            SpawnPoint = flag.transform.position;
            Destroy(flag);
        }
    }

    /*Mitä tarvitaan:
     * Box collider triggerit jokaiselle checkpointille
     * Checkpoint tagi 
     * Checkpoint system koodi Madisonille?
     * Vain Madison siirtyy takaisin
     * Checkpointit sijoittuvat ennen vesistöjä, jotta vältetään pelaajan turhautuminen ja ns. ylimääräinen kertaus.
    */
}
