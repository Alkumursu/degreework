using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadisonEntryMonologue : MonoBehaviour
{
    [SerializeField] GameObject dialogueBackground;
    [SerializeField] GameObject madisonSpeaks;

    void Start()
    {
        StartCoroutine(FirstMonologue());
    }

    void Update()
    {
        
    }

    IEnumerator FirstMonologue()
    {
        yield return new WaitForSeconds(1f);
        dialogueBackground.SetActive(true);
        madisonSpeaks.SetActive(true);
        yield return new WaitForSeconds(4f);
        dialogueBackground.SetActive(false);
        madisonSpeaks.SetActive(false);
        Destroy(dialogueBackground);
        Destroy(madisonSpeaks);
    }
}
