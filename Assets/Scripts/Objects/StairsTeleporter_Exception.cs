using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StairsTeleporter_Exception : MonoBehaviour
{
    //Collider col;
    [SerializeField] Vector3 endPosition;
    bool isHighlighted = false;
    public PlayerActions _playerActions;
    float fadeTime = 0.5f;
    ControllableCharacter cc;
    GameObject promptText;

    bool canTeleport;

    Collider col;

    public MadisonMonologue madMon;
    public bool allowTeleportation = false;

    void Start()
    {
        //col = GetComponent<Collider>();
        _playerActions = new PlayerActions();
        _playerActions.Player_Map.Enable();
        promptText = transform.GetChild(0).gameObject;
        canTeleport = false;
    }

    void Update()
    {
        if (madMon.madisonHasSpokenTwice == true)
        {
            allowTeleportation = true;
        }

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
        if (canTeleport == true)
        {
            FindObjectOfType<AudioManager>().Play("StairsSound");
            GameManager.Instance.FadeIn(fadeTime);
            //FindObjectOfType<AudioManager>().Play("StairsSound");
            yield return new WaitForSeconds(fadeTime * 2f);
            // Teleportation position
            cc.TeleportPosition(endPosition);
            yield return new WaitForSeconds(0.3f);
            GameManager.Instance.FadeOut(fadeTime * 2f);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && allowTeleportation == true)
        {
            cc = col.gameObject.GetComponent<ControllableCharacter>();

            isHighlighted = true;
            promptText.SetActive(true);
            canTeleport = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isHighlighted = false;
            //cc = null;
            promptText.SetActive(false);
            canTeleport = false;
        }
    }
}
