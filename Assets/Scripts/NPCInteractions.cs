using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractions : MonoBehaviour
{
    Collider _npcCol;
    GameObject _npcPromptText;

    void Start()
    {
        _npcPromptText = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _npcPromptText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _npcPromptText.SetActive(false);
        }
    }
}
