using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialogue : MonoBehaviour
{
    //Collider _col;
    [SerializeField] GameObject dialogueBackground1, dialogueBackground2;
    [SerializeField] GameObject madisonSpeaks, emmaSpeaks;

    private bool madisonHasSpoken;
    private bool emmaHasSpoken;

    void Start()
    {
        dialogueBackground1.SetActive(false);
        dialogueBackground2.SetActive(false);
        //Instantiate(dialogueBackground, new Vector3(0, 0, 0), Quaternion.identity);
        madisonHasSpoken = false;
        emmaHasSpoken = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Madison"))
        {
            dialogueBackground1.SetActive(true);
            madisonSpeaks.SetActive(true);
            StartCoroutine(MadisonDialogueCountdown());
        }
        if (collision.gameObject.CompareTag("Emma") && madisonHasSpoken == true)
        {
            dialogueBackground2.SetActive(true);
            emmaSpeaks.SetActive(true);
            StartCoroutine(EmmaDialogueCountdown());
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Madison") && madisonHasSpoken == true)
        {
            dialogueBackground1.SetActive(false);
            madisonSpeaks.SetActive(false);
            Destroy(dialogueBackground1);
            Destroy(madisonSpeaks);
        }
        if (collision.gameObject.CompareTag("Emma") && emmaHasSpoken == true)
        {
            dialogueBackground2.SetActive(false);
            emmaSpeaks.SetActive(false);
            Destroy(dialogueBackground2);
            Destroy(emmaSpeaks);
        }
    }

    IEnumerator MadisonDialogueCountdown()
    {
        yield return new WaitForSeconds(6f);
        dialogueBackground1.SetActive(false);
        madisonSpeaks.SetActive(false);
        madisonHasSpoken = true;
    }

    IEnumerator EmmaDialogueCountdown()
    {
        yield return new WaitForSeconds(6f);
        dialogueBackground2.SetActive(false);
        emmaSpeaks.SetActive(false);
        emmaHasSpoken = true;
    }
}
