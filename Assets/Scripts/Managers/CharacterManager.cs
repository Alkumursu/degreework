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
    public Vector3 cameraVelocity = Vector3.zero;
    float cameraX, cameraY = 0f;

    [SerializeField] float minCamX, minCamY, maxCamX, maxCamY;

    void Start()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;

        emmaCharacter.GetComponent<ControllableCharacter>().enabled = false;
        madisonCharacter.GetComponent<ControllableCharacter>().enabled = false;

        if (GameManager.Instance.State == GameState.EmmaActive)
        {
            currentCharacter = emmaCharacter;
        }
        else if (GameManager.Instance.State == GameState.MadisonActive)
        {
            currentCharacter = madisonCharacter;
        }

        cc = currentCharacter.GetComponent<ControllableCharacter>();
        _rb = currentCharacter.GetComponent<Rigidbody>();
        cc.enabled = true;
    }
 
    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;    
    }

    /* Testataan load ja save dataa, lopullisessa versiossa hahmon pit‰isi spawnata viimeisimp‰‰n check pointiin
    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }
    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
    }
    */

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
        cameraX = Mathf.Lerp(cameraX, _rb.velocity.x, Time.deltaTime);
        cameraY = Mathf.Lerp(cameraY, _rb.velocity.y, 0.25f * Time.deltaTime);
        cameraX = Mathf.Clamp(cameraX, -2.5f, 2.5f);
        cameraY = Mathf.Clamp(cameraY, -1, 1);
        Vector3 characterFollowPosition = new Vector3(currentCharacter.transform.position.x + cameraX,
            currentCharacter.transform.position.y + (cameraY * 1f), currentCharacter.transform.position.z);


        if (characterFollowPosition.x <= minCamX)
        {
            characterFollowPosition.x = minCamX;
        }

        if(characterFollowPosition.y <= minCamY - cameraOffset.y)
        {
            characterFollowPosition.y = minCamY - cameraOffset.y;
        }

        if (characterFollowPosition.x >= maxCamX)
        {
            characterFollowPosition.x = maxCamX;
        }

        if (characterFollowPosition.y >= maxCamY - cameraOffset.y)
        {
            characterFollowPosition.y = maxCamY - cameraOffset.y;
        }


        Vector3 targetPosition = characterFollowPosition + cameraOffset;
        cam.transform.position = Vector3.Slerp(cam.transform.position, targetPosition, cameraSmoothFactor * Time.deltaTime);
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
        cc._playerActions.Player_Map.Disable();
        cc.SetActivity(false);
        currentCharacter = newCharacter;
        cc = newCharacter.GetComponent<ControllableCharacter>();
        cc.enabled = true;
        _rb = newCharacter.GetComponent<Rigidbody>();
        cc._playerActions.Player_Map.Enable();
        cc.SetActivity(true);
    }
}
