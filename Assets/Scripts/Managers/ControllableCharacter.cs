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
    private float _crateDrag;

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

    CapsuleCollider col;

    //Crate
    FixedJoint joint;
    GameObject movableBox;
    bool isPushing;

    private void Awake()
    {
       GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    void Start()
    {
        _playerAnim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        _playerActions = new PlayerActions();
        _playerActions.Player_Map.Enable();
        currentPlayerState = PlayerState.Default;

        playerStateText.text = "Player state: " + currentPlayerState;
    }

    void ClearState()
    {
        submergence = 0f;
    }

    public void TeleportPosition(Vector3 endPosition)
    {
        transform.position = endPosition;
    }

    private void OnTriggerEnter (Collider other)
    {
        //Swimming
        if ((waterMask & (1 << other.gameObject.layer)) != 0)
        {
            EvaluateSubmergence();
            //Debug.Log("Water");
        }

        if (other.gameObject.CompareTag("WaterEntryLeft") && currentPlayerState == PlayerState.Swimming)
        {
            exitingWaterLeft = true;
            //Debug.Log("Exiting Water Left");
        }

        if (other.gameObject.CompareTag("WaterEntryRight") && currentPlayerState == PlayerState.Swimming)
        {
            exitingWaterRight = true;
            //Debug.Log("Exiting Water Right");
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
            //Debug.Log("Pushed Left");
        }

        if (exitingWaterRight)
        {
            Vector3 pushForce = new Vector3(1, 1, 0);
            _rb.AddForce(pushForce * 2, ForceMode.Acceleration);
            //Debug.Log("Pushed Right");
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
    }

    private void EvaluateSubmergence()
    {
        if (Physics.Raycast(_rb.position + upAxis * submergenceOffset,
            -upAxis, out RaycastHit hit, submergenceRange + 1f, waterMask, QueryTriggerInteraction.Collide))
        {
            submergence = 1f - hit.distance / submergenceRange;
            if(submergence < 0.66f)
            {
                col.direction = 1;
                currentPlayerState = PlayerState.Default;
                playerStateText.text = "Player state: " + currentPlayerState;
            }
        }
        else
        {
            col.direction = 2;
            submergence = 1f;
        }

        // How to call the currentCharacter functionality
        //metodien kutsumiseen gameObject.GetComponent<CharacterManager>().metodi();

        // Emma in water
        if (submergence >= 0.66f && GameManager.Instance.State == GameState.EmmaActive)
        {
            currentPlayerState = PlayerState.Swimming;
            _playerAnim.SetBool("Running", false);
            playerStateText.text = "Player state: " + currentPlayerState;
        }  
    }

    public void MadisonDeath()
    {
            currentPlayerState = PlayerState.Dying;
            _playerAnim.SetBool("Running", false);
            //_playerAnim.SetBool("Dying", true);
            playerStateText.text = "Player state: " + currentPlayerState;
            FindObjectOfType<GameManager>().HandleLoadCheckpoint();
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
            //_playerAnim.SetBool("JumpGrounded", true);
            _playerAnim.SetTrigger("Landing");

            /*_playerAnim.SetBool("Jumping", false);
            _jumping = false;
            _playerAnim.SetBool("JumpFalling", false);
            */
        }

        if (collision.gameObject.CompareTag("MovableCrate"))
        {
            movableBox = collision.gameObject;
            MeshRenderer mr = movableBox.GetComponent<MeshRenderer>();
            mr.material.color = Color.red;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Physics.Raycast(transform.position, Vector3.down, 2f))
        {
            _grounded = true;
            //_playerAnim.SetBool("JumpGrounded", true);
            /*_playerAnim.SetBool("Jumping", false);
            _jumping = false;
            _playerAnim.SetBool("JumpFalling", false);
            */
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _grounded = false;
        _playerAnim.SetBool("JumpGrounded", false);

        MeshRenderer mr = movableBox.GetComponent<MeshRenderer>();
        mr.material.color = Color.white;
        movableBox = null;
        mr = null;
    }

    public bool IsGrounded()
    {
        return _grounded;
    }

    private void FixedUpdate()
    {
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
        HandleCrateMoving();
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
        HandleCrateMoving();
    }

    void DyingAction()
    {
        HandleDying();
    }

    private void HandleMovement()
    {
        _playerActions.Player_Map.Jump.performed += _ => HandleJump();
        _playerActions.Player_Map.ChangeCharacter.performed += _ => TriggerCharacterChange();
        //_playerActions.Player_Map.StairsTeleportation.performed += _ => HandleTeleportation();

        _rb.drag = 0;
        _moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        _moveInput.y = _rb.velocity.y;
        Vector3 movementVelocity = new Vector3(_moveInput.x * _speed, _rb.velocity.y, 0);

        if(!_grounded)
        {
            movementVelocity.x = _rb.velocity.x;
        }

        _rb.velocity = Vector3.Lerp(_rb.velocity, movementVelocity, Time.fixedDeltaTime);

        if (_moveInput.x != 0 && _grounded)
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
        if (IsGrounded() && submergence < 1)
        {
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            _playerAnim.SetBool("Jumping", true);
        }
    }

    private void HandleSwimming()
    {
        _rb.drag = waterDrag;
        _moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        Vector3 movementVelocity = new Vector3(_moveInput.x * _speed, (_moveInput.y * _speed) * 0.33f, 0);
        _rb.velocity = Vector3.Lerp(_rb.velocity, movementVelocity, Time.fixedDeltaTime);
        _playerAnim.SetBool("SwimmingIdle", true);
        _playerAnim.SetBool("Swimming", false);

        if (_moveInput.x != 0)
        {
            _playerAnim.SetBool("SwimmingIdle", false);
            _playerAnim.SetBool("Swimming", true);
            Vector3 playerDir = new Vector3(_moveInput.x, 0, 0);
            Quaternion targetRotation = Quaternion.LookRotation(playerDir.normalized, Vector3.up);
            _rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 8f));
        }
    }

    private void HandleCrateMoving()
    {
        //_crateDrag = _playerActions.Player_Map.DragCrate

        // This is now toggle
        bool buttonPressed = _playerActions.Player_Map.DragCrate.WasPressedThisFrame();
        Debug.Log("State " + buttonPressed);

        if (joint == null && movableBox != null && buttonPressed)
        {
            //_playerAnim.SetBool("Pushing", true);
            Debug.Log("Fixed joint");
            joint = movableBox.AddComponent<FixedJoint>();
            joint.connectedBody = _rb;
        }

        else if(buttonPressed)
        {
            Debug.Log("Joint destroyed");
            Destroy(joint);
        }
    }

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
