using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI debugText;

    Rigidbody _rb;

    //Active character
    [SerializeField] GameObject emmaCharacter, madisonCharacter;
    private GameObject currentCharacter;
    private ControllableCharacter cc;

    //Camera
    public Vector3 cameraOffset;
    [Range(1, 10)]
    public float cameraSmoothFactor;
    [SerializeField] Camera cam;

    void Start()
    {
        Debug.Log("Hello");

        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;

        if (GameManager.Instance.State == GameState.EmmaActive)
        {
            currentCharacter = emmaCharacter;
        }
        else if(GameManager.Instance.State == GameState.MadisonActive)
        {
            currentCharacter = madisonCharacter;
        }

        cc = currentCharacter.GetComponent<ControllableCharacter>();
        _rb = currentCharacter.GetComponent<Rigidbody>();
    }
 
    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;    
    }

    private void Update()
    {
        debugText.text = GameManager.Instance.State.ToString();

    }

    private void LateUpdate()
    {
        CameraFollow();
    }

    private void CameraFollow()
    {
        Debug.Log("Camera works");
        Vector3 characterFollowPosition = new Vector3(currentCharacter.transform.position.x + _rb.velocity.x,
            currentCharacter.transform.position.y, currentCharacter.transform.position.z);
        Vector3 targetPosition = characterFollowPosition + cameraOffset;
        cam.transform.position = Vector3.Slerp(cam.transform.position, targetPosition, Time.deltaTime * cameraSmoothFactor);
        Debug.Log(cameraOffset.z);
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        if(state == GameState.EmmaActive)
        {
            SwitchTo(emmaCharacter);
        }
        else if(state == GameState.MadisonActive)
        {
            SwitchTo(madisonCharacter);
        }
    }

    private void SwitchTo(GameObject newCharacter)
    {
        //cc.StopMovement();
        cc._playerActions.Player_Map.Disable();
        cc.SetActivity(false);
        currentCharacter = newCharacter;
        cc = newCharacter.GetComponent<ControllableCharacter>();
        _rb = newCharacter.GetComponent<Rigidbody>();
        cc._playerActions.Player_Map.Enable();
        cc.SetActivity(true);
    }
}
