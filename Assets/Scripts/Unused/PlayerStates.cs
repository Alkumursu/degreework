using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStates : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI debugText;

    //Active character
    [SerializeField] GameObject emmaCharacter, madisonCharacter;
    private GameObject currentCharacter;
    private ControllableCharacter cc;
    
    //Player movement
    [SerializeField]
    private float _speed;
    private PlayerActions _playerActions;
    private Rigidbody _rb;
    private Vector2 _moveInput;

    //Camera
    public Vector3 cameraOffset;
    [Range(1, 10)]
    public float cameraSmoothFactor;
    [SerializeField] Camera cam;

    //Jumping
    [SerializeField]
    private float _jumpPower;
    private bool _grounded;

    //Crate
    [SerializeField]
    private float _pushPower;

    //Animations
    private Animator _playerAnim;

    //Player states MOVE TO CONTROLLABLE CHARACTER
    public enum PlayerState
    {
        Default,
        Ledge,
        Cord,
        Swimming,
        Diving
    }
    PlayerState currentPlayerState;

    void Start()
    {
        Debug.Log("Hello");

        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;

        if (GameManager.Instance.State == GameState.EmmaActive)
        {
            currentCharacter = emmaCharacter;
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameState.MadisonActive);
            currentCharacter = madisonCharacter;
        }

        _playerActions = new PlayerActions();
        _playerActions.Player_Map.Enable();
        _rb = currentCharacter.GetComponent<Rigidbody>();
        _playerAnim = currentCharacter.GetComponent<Animator>();
        cc = currentCharacter.GetComponent<ControllableCharacter>();
        currentPlayerState = PlayerState.Default;
    }
 
    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;    
    }

    private void LateUpdate()
    {
        debugText.text = GameManager.Instance.State.ToString();
        CameraFollow();
    }

    private void CameraFollow()
    {
        Vector3 targetPosition = currentCharacter.transform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, targetPosition, Time.deltaTime * cameraSmoothFactor);
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
        _rb.velocity = Vector3.zero;
        currentCharacter = newCharacter;
        cc = newCharacter.GetComponent<ControllableCharacter>();
        _rb = currentCharacter.GetComponent<Rigidbody>();
        _playerAnim = currentCharacter.GetComponent<Animator>();
    }

    /*private void OnEnable()
    {
        _playerActions = new PlayerActions();
        _playerActions.Player_Map.Enable();
    }*/

    private void OnDisable()
    {
        _playerActions.Player_Map.Disable();
    }

    private void FixedUpdate()
    {
        // Physics movement based on game states, input comes from elsewhere
        switch (currentPlayerState)
        {
            case PlayerState.Default:
                {
                    DefaultMovement();
                    break;
                }
            case PlayerState.Ledge:
                {
                    LedgeMovement();
                    break;
                }
            case PlayerState.Cord:
                {
                    CordMovement();
                    break;
                }
            case PlayerState.Swimming:
                {
                    SwimmingMovement();
                    break;
                }
            case PlayerState.Diving:
                {
                    DivingMovement();
                    break;
                }
        }
    }

    // CLASS METHODS //
    void DefaultMovement()
    {
        HandleMovement();
        //_playerActions.Player_Map.Jump.performed += _ => HandleJump();
        //_playerActions.Player_Map.ChangeCharacter.performed += _ => TriggerCharacterChange();
    }

    void LedgeMovement()
    {
        // Insert controller
        // GameManager.Instance.UpdateGameState(GameState.EmmaActive);
    }

    void CordMovement()
    {
        // Insert controller
    }

    void SwimmingMovement()
    {
        // Insert controller
    }

    void DivingMovement()
    {
        // Insert controller
    }

    // STATE CHANGES //
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ledge"))
        {
            currentPlayerState = PlayerState.Ledge;
        }
        else if (other.gameObject.CompareTag("Cord"))
        {
            currentPlayerState = PlayerState.Cord;
        }
        else if (other.gameObject.CompareTag("WaterSurface"))
        {
            currentPlayerState = PlayerState.Swimming;
        }
        else if (other.gameObject.CompareTag("WaterDeep"))
        {
            currentPlayerState = PlayerState.Diving;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ledge"))
        {
            currentPlayerState = PlayerState.Ledge;
        }
        else if (other.gameObject.CompareTag("Cord"))
        {
            currentPlayerState = PlayerState.Cord;
        }
        else if (other.gameObject.CompareTag("WaterSurface"))
        {
            currentPlayerState = PlayerState.Swimming;
        }
        else if (other.gameObject.CompareTag("WaterDeep"))
        {
            currentPlayerState = PlayerState.Diving;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ledge"))
        {
            currentPlayerState = PlayerState.Default;
        }
        else if (other.gameObject.CompareTag("Cord"))
        {
            currentPlayerState = PlayerState.Default;
        }
        else if (other.gameObject.CompareTag("WaterSurface"))
        {
            currentPlayerState = PlayerState.Default;
        }
        else if (other.gameObject.CompareTag("WaterDeep"))
        {
            currentPlayerState = PlayerState.Default;
        }
    }

    private void HandleMovement()
    {
        _playerActions.Player_Map.Jump.performed += _ => HandleJump();
        _playerActions.Player_Map.ChangeCharacter.performed += _ => TriggerCharacterChange();

        _moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        _moveInput.y = _rb.velocity.y;
        Vector3 movementVelocity = new Vector3(_moveInput.x * _speed, _rb.velocity.y, 0);
        _rb.velocity = Vector3.Lerp(_rb.velocity, movementVelocity, Time.fixedDeltaTime);

        if (_moveInput.x != 0)
        {
            _playerAnim.SetBool("Running", true);

            Vector3 playerDir = new Vector3(_moveInput.x, 0, 0);
            Quaternion targetRotation = Quaternion.LookRotation(playerDir, Vector3.up);
            _rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 8f));
        }
        else
        {
            _playerAnim.SetBool("Running", false);
        }
    }

    private void HandleJump()
    {
        Debug.Log("Jumped");

        if (cc.IsGrounded())
        {
            Debug.Log("Grounded");
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            _playerAnim.SetTrigger("Jumping");
        }
    }

    private void TriggerCharacterChange()
    {
        Debug.Log("Character change");

        if(GameManager.Instance.GetCharacterSwitchability() == false)
        {
            return;
        }

        if(GameManager.Instance.State == GameState.EmmaActive)
        {
            GameManager.Instance.UpdateGameState(GameState.MadisonActive);
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameState.EmmaActive);
        }
    }

    /*public void GrabLedge(Vector3 pos)
    {
        // should disable controller? "_player.enabled = false;"
        _playerAnim.SetBool("Holding", true);
        transform.position = pos;
    }
    */

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("MovableCrate"))
        {
            Rigidbody box = hit.collider.attachedRigidbody;

            if (!(box is null))
            {
                Vector3 moveDir = new Vector3(hit.moveDirection.x, 0, 0);
                box.velocity = moveDir * _pushPower;
            }
        }
    }
}
