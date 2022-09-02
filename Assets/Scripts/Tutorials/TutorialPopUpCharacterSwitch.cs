using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopUpCharacterSwitch : MonoBehaviour
{
    Collider _tutorialCol;
    GameObject _tutorialPromptText;

    public CharacterDialogue3 charDia;
    public bool tutorialPopUpAllowed = false;
    public bool madisonHasEnteredTheCollider = false;

    void Start()
    {
        _tutorialPromptText = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (charDia.dialogueHasEnded)
        {
            tutorialPopUpAllowed = true;
        }
        PermissionGiven();
    }

    private void OnTriggerEnter(Collider _tutorialCol)
    {
        if (_tutorialCol.gameObject.CompareTag("Madison"))
        {
            madisonHasEnteredTheCollider = true;
        }
    }

    public void PermissionGiven() 
    {
        if(madisonHasEnteredTheCollider && tutorialPopUpAllowed)
        {
            _tutorialPromptText.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider _tutorialCol)
    {
        if (_tutorialCol.gameObject.CompareTag("Madison"))
        {
            madisonHasEnteredTheCollider = false;
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
