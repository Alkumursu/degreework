using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCharacterChecker : MonoBehaviour
{
    //bool isActivated = false;

    bool emmaReachedEnd = false;
    bool madisonReachedEnd = false;

    [SerializeField] GameObject dialogueBackground;
    [SerializeField] GameObject unknownSpeaks;
    private bool unknownHasSpoken = false;


    void Start()
    {
        dialogueBackground.SetActive(false);
        unknownSpeaks.SetActive(false);
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Madison") && GameManager.Instance.State == GameState.MadisonActive)
        {
            madisonReachedEnd = true;
            GameEnding();
            Debug.Log("Madison reached end");
        }

        if (other.gameObject.CompareTag("Emma") && GameManager.Instance.State == GameState.EmmaActive)
        {
            emmaReachedEnd = true;
            GameEnding();
            Debug.Log("Emma reached end");
        }
    }
    /*private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Madison") && GameManager.Instance.State == GameState.MadisonActive)
        {
            madisonReachedEnd = true;
            GameEnding();
        }

        if (collision.gameObject.CompareTag("Emma") && GameManager.Instance.State == GameState.EmmaActive)
        {
            emmaReachedEnd = true;
            GameEnding();
        }
    }
    */

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Madison") && GameManager.Instance.State == GameState.MadisonActive)
        {
            madisonReachedEnd = false;
            GameEnding();
            Debug.Log("Madison missed the end");
        }

        if (other.gameObject.CompareTag("Emma") && GameManager.Instance.State == GameState.EmmaActive)
        {
            emmaReachedEnd = false;
            GameEnding();
            Debug.Log("Emma missed the end");
        }
    }

    public void GameEnding()
    {
        if (emmaReachedEnd == true && madisonReachedEnd == true)
        {
            Debug.Log("Game ends");
            FindObjectOfType<GameManager>().HandleGameWon();
        }
        else
        {
            if (unknownHasSpoken == false)
            {
                //Debug.Log("Both girls are needed");
                StartCoroutine(BothGirlsAreNeeded());
                //Inform player they need both girls present
            }
        }
    }
    IEnumerator BothGirlsAreNeeded()
    {
        dialogueBackground.SetActive(true);
        unknownSpeaks.SetActive(true);
        yield return new WaitForSeconds(3f);
        dialogueBackground.SetActive(false);
        unknownSpeaks.SetActive(false);
        unknownHasSpoken = true;
    }
}
    /*private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.CompareTag("Emma")) && (other.gameObject.CompareTag("Madison")))
        {
            isActivated = false;
        }
    }

    public bool IsActivated()
    {
        if(isActivated == true)
        {

        }
    }
    */
