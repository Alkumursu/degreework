using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameCharacterChecker : MonoBehaviour
{
    //bool isActivated = false;

    bool emmaReachedEnd = false;
    bool madisonReachedEnd = false;


    void Start()
    {

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
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Madison") && GameManager.Instance.State == GameState.MadisonActive)
        {
            madisonReachedEnd = true;
            GameEnding();
        }

        if (other.gameObject.CompareTag("Emma") && GameManager.Instance.State == GameState.EmmaActive)
        {
            emmaReachedEnd = true;
            GameEnding();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("EndingTrigger") && GameManager.Instance.State == GameState.MadisonActive)
        {
            madisonReachedEnd = false;
            GameEnding();
            Debug.Log("Madison missed the end");
        }

        if (other.gameObject.CompareTag("EndingTrigger") && GameManager.Instance.State == GameState.EmmaActive)
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
            Debug.Log("Both girls are needed");
            //Inform player they need both girls present
        }

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
