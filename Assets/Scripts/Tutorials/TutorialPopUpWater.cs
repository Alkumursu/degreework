using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopUpWater : MonoBehaviour
{
    Collider _tutorialCol;
    GameObject _tutorialPromptText;

    void Start()
    {
        _tutorialPromptText = transform.GetChild(0).gameObject;
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider _tutorialCol)
    {
        if (_tutorialCol.gameObject.CompareTag("Emma"))
        {
            _tutorialPromptText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider _tutorialCol)
    {
        if (_tutorialCol.gameObject.CompareTag("Emma"))
        {
            StartCoroutine(TutorialFadeOut());
        }
    }

    IEnumerator TutorialFadeOut()
    {
        yield return new WaitForSeconds(1f);
        _tutorialPromptText.SetActive(false);
        Destroy(_tutorialPromptText);
    }
}
