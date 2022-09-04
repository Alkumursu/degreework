using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialogue3 : MonoBehaviour
{
    [SerializeField] GameObject dialogueBackground;
    [SerializeField] GameObject madisonSpeaks, emmaSpeaks;

    public bool dialogueHasEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBackground.SetActive(false);
        madisonSpeaks.SetActive(false);
        emmaSpeaks.SetActive(false);
}

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Madison"))
        {
            StartCoroutine(DialogueCountdown());
        }
    }

    IEnumerator DialogueCountdown()
    {
        dialogueBackground.SetActive(true);
        madisonSpeaks.SetActive(true);
        yield return new WaitForSeconds(6f);
        madisonSpeaks.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        emmaSpeaks.SetActive(true);
        yield return new WaitForSeconds(6f);
        dialogueBackground.SetActive(false);
        emmaSpeaks.SetActive(false);
        Destroy(dialogueBackground);
        Destroy(emmaSpeaks);
        Destroy(madisonSpeaks);
        dialogueHasEnded = true;
    }
}
