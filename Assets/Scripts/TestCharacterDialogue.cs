using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TestCharacterDialogue : MonoBehaviour
{
    //Tämä koodi on kesken

    /*
    TextMeshProUGUI dialogueText;
    [SerializeField] GameObject dialogueBackground;

    public enum CharacterLine
    {
        StandBy,
        MadisonLine1,
        EmmaLine1
    }

    CharacterLine characterLine;

    void Start()
    {
        //dialogueBackground = transform.GetChild(0).gameObject;
        dialogueText.text = "Dialogue: " + characterLine;
        characterLine = CharacterLine.StandBy;
    }

    private void FixedUpdate()
    {
        switch (characterLine)
        {
            case CharacterLine.StandBy:
                {
                    StandByState();
                    break;
                }
            case CharacterLine.MadisonLine1:
                {
                    MadisonLineState();
                    break;
                }
            case CharacterLine.EmmaLine1:
                {
                    EmmaLineState();
                    break;
                }
        }
    }


    private void StandByState()
    {
        dialogueBackground.SetActive(false);
    }

    private void MadisonLineState()
    {
        dialogueBackground.SetActive(true);
        characterLine = CharacterLine.MadisonLine1;
    }

    private void EmmaLineState()
    {
        throw new NotImplementedException();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Madison"))
        {
            dialogueBackground.SetActive(true);
            characterLine = CharacterLine.MadisonLine1;
            //dialogueText1.text = "Madison sanoo jotain";
        }
        if (collision.gameObject.CompareTag("Emma"))
        {
            dialogueBackground.SetActive(true);
            characterLine = CharacterLine.EmmaLine1;
            //dialogueText2.text = "Emma sanoo jotain";
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Madison"))
        {
            characterLine = CharacterLine.StandBy; 
        }
        if (collision.gameObject.CompareTag("Emma"))
        {
            characterLine = CharacterLine.StandBy;
        }
        if (collision.gameObject.CompareTag("Emma") && collision.gameObject.CompareTag("Madison"))
        {
            dialogueBackground.SetActive(false);

        }
    }*/
}
