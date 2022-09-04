using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonDoorFunctioning_ExceptionDoor : MonoBehaviour
{
    bool isHighlighted = false;
    public PlayerActions _playerActions;
    GameObject doorPromptText;
    Animator _anim;

    private bool doorIsOpen;
    private bool canOpenDoor;
    [SerializeField] Light pointLight;

    void Start()
    {
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
        if (isHighlighted && canOpenDoor == true && this != null)
        {
            //Debug.Log("Door opened");
            _playerActions.Player_Map.Interact.performed += _ => ActivateButton();
        }
    }

    void ActivateButton()
    {
        if (doorIsOpen == false)
        {
            _anim.Play("DoorSlideOpen");
            FindObjectOfType<AudioManager>().Play("ButtonSound");
            FindObjectOfType<AudioManager>().Play("DoorSound");

            pointLight.DOIntensity(0, 0.2f);
            doorIsOpen = true;
            isHighlighted = false;
            canOpenDoor = false;
            Destroy(doorPromptText);
            Destroy(this);
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
