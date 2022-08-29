using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonDoorFunctioning : MonoBehaviour
{
    Collider col;
    bool isHighlighted = false;
    public PlayerActions _playerActions;
    //ControllableCharacter cc;
    GameObject doorPromptText;
    Animator _anim;

    private bool doorIsOpen;
    private bool canOpenDoor;
    [SerializeField] Light pointLight;

    void Start()
    {
        //col = GetComponent<Collider>();
        doorPromptText = transform.GetChild(0).gameObject;
        _playerActions = new PlayerActions();
        _playerActions.Player_Map.Enable();
        _anim = this.GetComponent<Animator>();

        doorIsOpen = false;
        canOpenDoor = false;
        pointLight.DOIntensity(1, 0.2f);
    }

    void Update()
    {
        if (isHighlighted && canOpenDoor == true)
        {
            Debug.Log("Door opened");
            _playerActions.Player_Map.Interact.performed += _ => ActivateButton();
        }
    }

    void ActivateButton()
    {
        if(doorIsOpen == false)
        {
            //_anim.SetTrigger("DoorTrigger");
            _anim.Play("DoorSlideOpen");
            //FindObjectOfType<AudioManager>().Play("DoorSound");

            pointLight.DOIntensity(0, 0.2f);
            doorIsOpen = true;
        }

        else if(doorIsOpen == true)
        {
            //_anim.SetTrigger("DoorTrigger");
            _anim.Play("DoorSlideClose");
            //FindObjectOfType<AudioManager>().Play("DoorSound");

            pointLight.DOIntensity(1, 0.2f);
            doorIsOpen = false;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isHighlighted = true;
            doorPromptText.SetActive(true);
            canOpenDoor = true;
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isHighlighted = true;
            //doorPromptText.SetActive(true);
        }
    }
    */
    
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isHighlighted = false;
            doorPromptText.SetActive(false);
            canOpenDoor = false;
        }
    }
}
