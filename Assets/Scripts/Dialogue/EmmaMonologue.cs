using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmmaMonologue : MonoBehaviour
{
    [SerializeField] GameObject dialogueBackground;
    [SerializeField] GameObject emmaSpeaks;

    void Start()
    {
        dialogueBackground.SetActive(false);
        emmaSpeaks.SetActive(false);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Emma"))
        {
            dialogueBackground.SetActive(true);
            emmaSpeaks.SetActive(true);
            StartCoroutine(EmmaMonologueCountdown());
        }
    }

    IEnumerator EmmaMonologueCountdown()
    {
        yield return new WaitForSeconds(6f);
        dialogueBackground.SetActive(false);
        emmaSpeaks.SetActive(false);
        Destroy(dialogueBackground);
        Destroy(emmaSpeaks);
    }
}
