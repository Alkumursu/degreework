using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadisonMonologue2 : MonoBehaviour
{
    [SerializeField] GameObject dialogueBackground;
    [SerializeField] GameObject madisonSpeaks;

    void Start()
    {
        dialogueBackground.SetActive(false);
        madisonSpeaks.SetActive(false);
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Madison"))
        {
            dialogueBackground.SetActive(true);
            madisonSpeaks.SetActive(true);
            StartCoroutine(MadisonMonologueCountdown());
        }
    }

    IEnumerator MadisonMonologueCountdown()
    {
        yield return new WaitForSeconds(6f);
        dialogueBackground.SetActive(false);
        madisonSpeaks.SetActive(false);
        Destroy(dialogueBackground);
        Destroy(madisonSpeaks);
    }
}
