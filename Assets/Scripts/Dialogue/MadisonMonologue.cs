using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadisonMonologue : MonoBehaviour
{
    [SerializeField] GameObject dialogueBackground;
    [SerializeField] GameObject madisonSpeaks1, madisonSpeaks2;

    private bool madisonHasSpokenOnce;
    private bool madisonHasSpokenTwice;


    void Start()
    {
        dialogueBackground.SetActive(false);
        madisonHasSpokenOnce = false;
        madisonHasSpokenTwice = false;
        madisonSpeaks1.SetActive(false);
        madisonSpeaks2.SetActive(false);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Madison"))
        {
            dialogueBackground.SetActive(true);
            madisonSpeaks1.SetActive(true);
            StartCoroutine(MadisonMonologueCountdown1());
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Madison") && madisonHasSpokenTwice == true)
        {
            dialogueBackground.SetActive(false);
            madisonSpeaks2.SetActive(false);
            Destroy(dialogueBackground);
            Destroy(madisonSpeaks2);
            
        }
    }

    private void MadisonHasSpokenOnce()
    {
        madisonSpeaks2.SetActive(true);
        StartCoroutine(MadisonMonologueCountdown2());
    }

    IEnumerator MadisonMonologueCountdown1()
    {
        yield return new WaitForSeconds(4f);
        //dialogueBackground.SetActive(false);
        madisonSpeaks1.SetActive(false);
        madisonHasSpokenOnce = true;
        Destroy(madisonSpeaks1);
        MadisonHasSpokenOnce();
    }
    IEnumerator MadisonMonologueCountdown2()
    {
        yield return new WaitForSeconds(4f);
        dialogueBackground.SetActive(false);
        madisonSpeaks2.SetActive(false);
        madisonHasSpokenTwice = true;
    }
}
