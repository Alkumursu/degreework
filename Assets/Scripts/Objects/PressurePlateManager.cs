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

    void Start()
    {
        _anim = this.GetComponent<Animator>();
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
                //tähän valot tms.
                //käytetään dotweeniä valon intensity sisään ja pois
                lights[i].DOIntensity(1, 0.2f);
                doorLights[i].DOIntensity(1, 0.2f);
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
            _anim.CrossFade("DoorSlideOpen", 0.2f);
            Debug.Log("DoorOpen");
        }
        else
        {
            //sulje ovet
            _anim.CrossFade("DoorSlideClose", 0.2f);
            Debug.Log("DoorClose");

        }
    }
}
