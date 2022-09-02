using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialogue2 : MonoBehaviour
{
    [SerializeField] GameObject dialogueBackground;
    [SerializeField] GameObject madisonSpeaks1, madisonSpeaks2, emmaSpeaks;

    //private bool madisonHasSpoken;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBackground.SetActive(false);
        madisonSpeaks1.SetActive(false);
        madisonSpeaks2.SetActive(false);
        emmaSpeaks.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Madison"))
        {
            StartCoroutine(DialogueCountdown());
        }
    }

    IEnumerator DialogueCountdown()
    {
        dialogueBackground.SetActive(true);
        madisonSpeaks1.SetActive(true);
        yield return new WaitForSeconds(6f);
        madisonSpeaks1.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        emmaSpeaks.SetActive(true);
        yield return new WaitForSeconds(6f);
        emmaSpeaks.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        madisonSpeaks2.SetActive(true);
        yield return new WaitForSeconds(4f);
        madisonSpeaks2.SetActive(false);
        dialogueBackground.SetActive(false);
        Destroy(dialogueBackground);
        Destroy(emmaSpeaks);
        Destroy(madisonSpeaks1);
        Destroy(madisonSpeaks2);
    }
}
