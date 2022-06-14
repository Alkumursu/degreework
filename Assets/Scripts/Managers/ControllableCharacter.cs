using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllableCharacter : MonoBehaviour
{
    bool active;
    [SerializeField] TextMeshProUGUI playerStateText;

    //Player movement
    [SerializeField]
    private float _speed;
    public PlayerActions _playerActions;
    private Rigidbody _rb;
    private Vector2 _moveInput;

    //Walk-run transition test WIP
    /*public float moveSpeed = 1.5f;
    public float walkSpeed = 1.5f;
    public float runSpeed = 8.0f;

    private bool walking = true;
    */

    //Jumping
    [SerializeField]
    private float _jumpPower;
    private bool _grounded;

    //Crate
    [SerializeField]
    private float _pushPower;

    //Animations
    private Animator _playerAnim;

    //Player states
    public enum PlayerState
    {
        Default,
        Ledge,
        Cord,
        Swimming,
        Dying
    }
    PlayerState currentPlayerState;

    //Water
    [SerializeField] LayerMask waterMask;
    bool InWater => submergence > 0f;
    float submergence;
    [SerializeField] float submergenceOffset = 0.5f;
    [SerializeField, Min(0.1f)]
    float submergenceRange = 1f;

    [SerializeField, Range(0f, 10f)]
    float waterDrag = 1f;

    [SerializeField, Min(0f)]
    float buoyancy = 1f;

    bool exitingWaterLeft = false;
    bool exitingWaterRight = false;

    Vector3 upAxis = new Vector3(0, 1, 0);

    //Stairs
    [SerializeField] GameObject player;
    public GameObject TeleportTo1, TeleportTo2, TeleportTo3, TeleportTo4;

    bool toUpstairs1 = false;
    bool toDownstairs1 = false;
    bool toUpstairs2 = false;
    bool toDownstairs2 = false;

    //Teleportation prompt
    public GameObject tpPrompt;

    private void Awake()
    {
       GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    void Start()
    {
        _playerAnim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();

        _playerActions = new PlayerActions();
        _playerActions.Player_Map.Enable();
        currentPlayerState = PlayerState.Default;

        playerStateText.text = "Player state: " + currentPlayerState;

        tpPrompt.SetActive(false);
    }

    void ClearState()
    {
        submergence = 0f;
    }

    private void OnTriggerEnter (Collider other)
    {
        //Swimming
        if ((waterMask & (1 << other.gameObject.layer)) != 0)
        {
            EvaluateSubmergence();
            Debug.Log("Water");
        }

        if (other.gameObject.CompareTag("WaterEntryLeft") && currentPlayerState == PlayerState.Swimming)
        {
            exitingWaterLeft = true;
            Debug.Log("Exiting Water Left");
        }

        if (other.gameObject.CompareTag("WaterEntryRight") && currentPlayerState == PlayerState.Swimming)
        {
            exitingWaterRight = true;
            Debug.Log("Exiting Water Right");
        }


        //Teleportation
        if (other.gameObject.CompareTag("StairTeleporter1"))
        {
            toUpstairs1 = true;
            tpPrompt.SetActive(true);
        }
        if (other.gameObject.CompareTag("StairTeleporter2"))
        {
            toDownstairs1 = true;
            tpPrompt.SetActive(true);
        }
        if (other.gameObject.CompareTag("StairTeleporter3"))
        {
            toUpstairs2 = true;
            tpPrompt.SetActive(true);
        }
        if (other.gameObject.CompareTag("StairTeleporter4"))
        {
            toDownstairs2 = true;
            tpPrompt.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if ((waterMask & (1 << other.gameObject.layer)) != 0)
        {
            EvaluateSubmergence();
        }

        if (exitingWaterLeft)
        {
            Vector3 pushForce = new Vector3(-1, 1, 0);
            _rb.AddForce(pushForce * 2, ForceMode.Acceleration);
            Debug.Log("Pushed Left");
        }

        if (exitingWaterRight)
        {
            Vector3 pushForce = new Vector3(1, 1, 0);
            _rb.AddForce(pushForce * 2, ForceMode.Acceleration);
            Debug.Log("Pushed Right");
        }

        if (other.gameObject.CompareTag("StairTeleporter1"))
        {
            toUpstairs1 = true;
            tpPrompt.SetActive(true);
        }
        if (other.gameObject.CompareTag("StairTeleporter2"))
        {
            toDownstairs1 = true;
            tpPrompt.SetActive(true);
        }
        if (other.gameObject.CompareTag("StairTeleporter3"))
        {
            toUpstairs2 = true;
            tpPrompt.SetActive(true);
        }
        if (other.gameObject.CompareTag("StairTeleporter4"))
        {
            toDownstairs2 = true;
            tpPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("WaterEntryLeft"))
        {
            exitingWaterLeft = false;
        }

        if (other.gameObject.CompareTag("WaterEntryRight"))
        {
            exitingWaterRight = false;
        }

        //Teleportation
        if (other.gameObject.CompareTag("StairTeleporter1"))
        {
            toUpstairs1 = false;
            tpPrompt.SetActive(false);
        }
        if (other.gameObject.CompareTag("StairTeleporter2"))
        {
            toDownstairs1 = false;
            tpPrompt.SetActive(false);
        }
        if (other.gameObject.CompareTag("StairTeleporter3"))
        {
            toUpstairs2 = false;
            tpPrompt.SetActive(false);
        }
        if (other.gameObject.CompareTag("StairTeleporter4"))
        {
            toDownstairs2 = false;
            tpPrompt.SetActive(false);
        }

    }

    private void EvaluateSubmergence()
    {
        if (Physics.Raycast(_rb.position + upAxis * submergenceOffset,
            -upAxis, out RaycastHit hit, submergenceRange + 1f, waterMask, QueryTriggerInteraction.Collide))
        {
            submergence = 1f - hit.distance / submergenceRange;
            if(submergence < 0.66f)
            {
                currentPlayerState = PlayerState.Default;
                playerStateText.text = "Player state: " + currentPlayerState;
            }
        }
        else
        {
            submergence = 1f;
        }

        // How to call the currentCharacter functionality
        //metodien kutsumiseen gameObject.GetComponent<CharacterManager>().metodi();

        // Emma in water
        if (submergence >= 0.66f && GameManager.Instance.State == GameState.EmmaActive)
        {
            currentPlayerState = PlayerState.Swimming;
            _playerAnim.SetBool("Running", false);
            //_playerAnim.SetBool("Swimming", true);
            playerStateText.text = "Player state: " + currentPlayerState;
        }  

        // Madison in water
        if (submergence >= 0.66f && GameManager.Instance.State == GameState.MadisonActive)
        {
            currentPlayerState = PlayerState.Dying;
            _playerAnim.SetBool("Running", false);
            //_playerAnim.SetBool("Dying", true);
            playerStateText.text = "Player state: " + currentPlayerState;
            // The row below probably needs to be reorganized to character manager script
            FindObjectOfType<GameManager>().HandleLoadCheckpoint();

        }
    }

    private void GameManagerOnOnGameStateChanged(GameState newState)
    {
        
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }

    public void SetActivity(bool isActive)
    {
        active = isActive;
    }

    public bool GetActivity()
    {
        return active;
    }

    public void StopMovement()
    {
        _rb.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Physics.Raycast(transform.position, Vector3.down, 2f))
        {
            _grounded = true;
            _playerAnim.SetTrigger("Landing");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Physics.Raycast(transform.position, Vector3.down, 2f))
        {
            _grounded = true;
            _playerAnim.SetTrigger("Landing");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _grounded = false;
    }

    public bool IsGrounded()
    {
        return _grounded;
    }

    private void FixedUpdate()
    {
        /*if (!active)
        {
            return;
        }*/

        if (InWater)
        {
            _rb.velocity += Physics.gravity * ((1f - buoyancy * submergence) * Time.deltaTime);
        }

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
            case PlayerState.Dying:
                {
                    DyingAction();
                    break;
                }
        }
    }

    // CLASS METHODS //
    void DefaultMovement()
    {
        HandleMovement();
        //HandleCrateMoving();
    }


    void LedgeMovement()
    {
        // Insert controller
    }

    void CordMovement()
    {
        // Insert controller
    }

    void SwimmingMovement()
    {
        HandleSwimming();
        //HandleCrateMoving();
    }

    void DyingAction()
    {
        HandleDying();
    }

    private void HandleMovement()
    {
        _playerActions.Player_Map.Jump.performed += _ => HandleJump();
        _playerActions.Player_Map.ChangeCharacter.performed += _ => TriggerCharacterChange();
        _playerActions.Player_Map.StairsTeleportation.performed += _ => HandleTeleportation();

        _rb.drag = 0;
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

        if (IsGrounded() && submergence < 1)
        {
            Debug.Log("Grounded");
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            _playerAnim.SetTrigger("Jumping");
        }
    }

    private void HandleSwimming()
    {
        _rb.drag = waterDrag;
        _moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        //_moveInput.y = 0f;
        Vector3 movementVelocity = new Vector3(_moveInput.x * _speed, (_moveInput.y * _speed) * 0.33f, 0);
        _rb.velocity = Vector3.Lerp(_rb.velocity, movementVelocity, Time.fixedDeltaTime);

        if (_moveInput.x != 0)
        {
            Vector3 playerDir = new Vector3(_moveInput.x, 0, 0);
            Quaternion targetRotation = Quaternion.LookRotation(playerDir.normalized, Vector3.up);
            _rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 8f));
        }
    }

    private void HandleTeleportation()
    {
        if (toUpstairs1 == true)
        {
            player.transform.position = TeleportTo1.transform.position;
        }   

        if (toDownstairs1 == true)
        {
            player.transform.position = TeleportTo2.transform.position; 
        }

        if (toUpstairs2 == true)
        {
            player.transform.position = TeleportTo3.transform.position;
        }

        if (toDownstairs2 == true)
        {
            player.transform.position = TeleportTo4.transform.position;
        }
    }

    /*private void HandleCrateMoving()
    {
        _moveInput = _playerActions.Player_Map.DragCrate.ReadValue<Vector2>();

        
    }
    */

    public void HandleDying()
    {

    }

    private void TriggerCharacterChange()
    {
        GameManager.Instance.SetCharacterSwitchability(true);

        if (GameManager.Instance.GetCharacterSwitchability() == false)
        {
            return;
        }

        if (GameManager.Instance.State == GameState.EmmaActive)
        {
            GameManager.Instance.UpdateGameState(GameState.MadisonActive);
        }
        else if(GameManager.Instance.State == GameState.MadisonActive)
        {
            GameManager.Instance.UpdateGameState(GameState.EmmaActive);
        }
        
        Debug.Log("Character change" + GameManager.Instance.State);
    }

    /*public void GrabLedge(Vector3 pos)
    {
        // should disable controller? "_player.enabled = false;"
        _playerAnim.SetBool("Holding", true);
        transform.position = pos;
    }*/

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
