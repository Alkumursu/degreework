using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StairsTeleporter : MonoBehaviour
{
    //Collider col;
    [SerializeField] Vector3 endPosition;
    bool isHighlighted = false;
    public PlayerActions _playerActions;
    float fadeTime = 0.5f;
    ControllableCharacter cc;
    GameObject promptText;

    bool teleported = false;

    void Start()
    {
        //col = GetComponent<Collider>();
        _playerActions = new PlayerActions();
        _playerActions.Player_Map.Enable();
        promptText = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (isHighlighted)
        {
            Debug.Log("Stairs ready");
            _playerActions.Player_Map.StairsTeleportation.performed += _ => GoToEnd();
        }
    }

    void GoToEnd()
    {
        Debug.Log("Success");
        StartCoroutine(TeleportSequence());
    }

    IEnumerator TeleportSequence()
    {
        GameManager.Instance.FadeIn(fadeTime);
        yield return new WaitForSeconds(fadeTime);
        // Teleportation position
        cc.TeleportPosition(endPosition);
        yield return new WaitForSeconds(0.3f);
        GameManager.Instance.FadeOut(fadeTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cc = collision.gameObject.GetComponent<ControllableCharacter>();

            isHighlighted = true;
            promptText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isHighlighted = false;
            cc = null;
            promptText.SetActive(false);
        }
    }
}
