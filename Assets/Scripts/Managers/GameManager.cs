using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    //Characters
    bool canSwitch;
    bool characterHasDied = false;

    //Scenes
    public float restartDelay = 1f;
    [SerializeField] Image fadeToBlack;
    //[SerializeField] float sceneLoadDelay = 3f;
    float fadeTime = 0.5f;

    //Ending screen
    public static bool gameIsWon;
    public GameObject gameWonScreen;

    //Pause Menu
    public bool allowPauseMenu;

    private void Awake()
    {
        Instance = this;

        //testi
        //FindObjectOfType<AudioManager>().StopPlaying("MenuMusic");
        //FindObjectOfType<AudioManager>().Play("BGM");
    }
    void Start()
    {
        UpdateGameState(GameState.MadisonActive);
        gameIsWon = false;
        allowPauseMenu = true;
    }

    public void FadeIn(float fadeTime)
    {
        fadeToBlack.DOFade(1f, fadeTime);
    }

    public void FadeOut(float fadeTime)
    {
        fadeToBlack.DOFade(0f, fadeTime);
    }


    public void SetCharacterSwitchability(bool pSwitchability)
    {
        canSwitch = pSwitchability;
    }

    public bool GetCharacterSwitchability()
    {
        return canSwitch;
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (State)
        {
            /*case GameState.MainMenu:
                HandleMainMenu();
                break;
            */
            case GameState.PauseMenu:
                HandlePauseMenu();
                break;
            case GameState.MadisonActive:
                HandleMadisonActive();
                break;
            case GameState.EmmaActive:
                HandleEmmaActive();
                break;
            case GameState.GameWon:
                HandleGameWon();
                break;
            case GameState.LoadCheckpoint:
                HandleLoadCheckpoint();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(State), State, null);
        }

        OnGameStateChanged?.Invoke(State);
    }

    /*private void HandleMainMenu()
    {

    }
    */

    public void HandlePauseMenu()
    {
        //Debug.Log("Pause menu activated");
        //pauseScreen.gameObject.SetActive(true);
        //Time.timeScale = 0f;
    }
    

    private void HandleMadisonActive()
    {

    }

    private void HandleEmmaActive()
    {

    }

    public void HandleGameWon()
    {
        gameIsWon = !gameIsWon;
        allowPauseMenu = false;

        if (gameIsWon)
        {
            Debug.Log("Manager is Winning");
            gameWonScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        /*else
        {
            gameWonScreen.SetActive(false);
            Time.timeScale = 1f;
        }
        */
    }

    public void HandleContinue()
    {
        if (characterHasDied == false)
        {
            FindObjectOfType<ControllableCharacter>().CanClosePause();
        }
    }

    public void CharacterIsDead()
    {
        FindObjectOfType<AudioManager>().Play("MadisonDeathSound");
    }

    public void HandleLoadCheckpoint()
    {
        if (characterHasDied == false)
        {
            characterHasDied = true;
            CharacterIsDead();
            //Debug.Log("Reload scene");
            Invoke("Restart", restartDelay);
        }
    }

    public void Restart()
    {
        if (characterHasDied == false && gameIsWon == false)
        {
            FindObjectOfType<ControllableCharacter>().TriggerPauseMenu();
        }

        StartCoroutine(RestartSequence());
        
        /*
        fadeToBlack.DOFade(1f, sceneLoadDelay);
        DOTween.KillAll();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        */
        
    }

    public void ReturnMainMenu()
    {
        if (characterHasDied == false && gameIsWon == false)
        {
            FindObjectOfType<ControllableCharacter>().TriggerPauseMenu();
        }

        StartCoroutine(LoadMainMenu());

        /*fadeToBlack.DOFade(1f, sceneLoadDelay);
        FadeIn(fadeTime);
        DOTween.KillAll();
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        FadeOut(fadeTime);
        */

        //FindObjectOfType<AudioManager>().StopPlaying("BGM");
        //FindObjectOfType<AudioManager>().Play("MenuMusic");
    }

    IEnumerator LoadMainMenu()
    {
        Time.timeScale = 1f;
        FadeIn(fadeTime);
        yield return new WaitForSeconds(0.3f);
        DOTween.KillAll();
        SceneManager.LoadScene(0);

        //yield return new WaitForSeconds(0.3f);
        //FadeOut(fadeTime);
    }

    IEnumerator RestartSequence()
    {
        if (characterHasDied == false)
        {
            Time.timeScale = 1f;
            FadeIn(fadeTime);
            yield return new WaitForSeconds(1); //ennen 0.3f
            DOTween.KillAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Time.timeScale = 1f;
            FadeIn(fadeTime);
            yield return new WaitForSeconds(3); //ennen 0.3f
            DOTween.KillAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /*yield return new WaitForSeconds(0.3f);
        fadeToBlack.DOFade(1f, sceneLoadDelay);
        FadeOut(fadeTime);
        DOTween.KillAll();
        */

        //FindObjectOfType<AudioManager>().StopPlaying("BGM");
        //FindObjectOfType<AudioManager>().Play("MenuMusic");
    }
}

public enum GameState
{
    MainMenu,
    PauseMenu,
    MadisonActive,
    EmmaActive,
    GameWon,
    LoadCheckpoint
}
