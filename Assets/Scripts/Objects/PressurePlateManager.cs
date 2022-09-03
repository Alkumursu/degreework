using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PressurePlateManager : MonoBehaviour
{
    public PressurePlate[] pressurePlates;
    public Light[] lights;
    public Light[] doorLights;
    Animator _anim;
    private bool doorIsOpen;

    void Start()
    {
        _anim = this.GetComponent<Animator>();
        doorIsOpen = false;
    }

    void Update()
    {
        
    }

    public void CheckAllPlates()
    {
        bool failure = false;

        for (int i = 0; i < pressurePlates.Length; i++)
        {
            if (pressurePlates[i].IsActivated())
            {
                //With dotween we manage the intensity of lights
                lights[i].DOIntensity(2, 0.2f);
                doorLights[i].DOIntensity(2, 0.2f);
                //voit lisätä väriä
            }
            else
            {
                failure = true;
                lights[i].DOIntensity(0, 0.2f);
                doorLights[i].DOIntensity(0, 0.2f);
            }
        } 

        if (!failure)
        {
            //avaa ovet
            doorIsOpen = true;
            _anim.Play("DoorSlideOpen");
            FindObjectOfType<AudioManager>().Play("DoorSound");
            Debug.Log("DoorOpen");
        }
        else if (doorIsOpen == true)
        {
            _anim.Play("DoorSlideClose");
            FindObjectOfType<AudioManager>().Play("DoorSound");
            Debug.Log("DoorClose");
            doorIsOpen = false;
        }
    }
}
