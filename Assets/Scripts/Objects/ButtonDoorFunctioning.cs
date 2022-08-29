using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonDoorFunctioning : MonoBehaviour
{
    //Collider col;
    bool isHighlighted = false;
    public PlayerActions _playerActions;
    //ControllableCharacter cc;
    GameObject doorPromptText;
    Animator _anim;

    void Start()
    {
        //col = GetComponent<Collider>();
        doorPromptText = transform.GetChild(0).gameObject;
        _playerActions = new PlayerActions();
        _playerActions.Player_Map.Enable();
        _anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (isHighlighted)
        {
            Debug.Log("Door opened");
            _playerActions.Player_Map.Interact.performed += _ => ActivateButton();
        }
    }

    void ActivateButton()
    {
        _anim.SetTrigger("DoorTrigger");
        FindObjectOfType<AudioManager>().Play("DoorSound");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isHighlighted = true;
            doorPromptText.SetActive(true);
        }
    }

    /*private void OnTriggerStay(Collider collision)
    {
        
    }
    */

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isHighlighted = false;
            doorPromptText.SetActive(false);
        }
    }
}
