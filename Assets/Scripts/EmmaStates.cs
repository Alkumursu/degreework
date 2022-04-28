using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmmaStates : MonoBehaviour
{
    //Player movement
    [SerializeField]
    private float _speed;
    private PlayerActions _playerActions;
    private Rigidbody _rb;
    private Vector2 _moveInput;

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
        //Idle,
        Default,
        //Jumping,
        Ledge,
        Cord,
        Swimming,
        Diving
        //Dying
    }
    PlayerState currentPlayerState;

    void Awake()
    {
        _playerActions = new PlayerActions();
        _rb = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();
        //GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }
 
    /*void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;    
    }
    */

    void Start()
    {
        currentPlayerState = PlayerState.Default;
    }

    /*private void GameManagerOnOnGameStateChanged(GameState state)
    {
        lisääGameObjectTms.SetActive(state == GameState.OnlyEmma);
    }
    */

    private void OnEnable()
    {
        _playerActions.Player_Map.Enable();
    }

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
        _playerActions.Player_Map.Jump.performed += _ => HandleJump();
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
        if (_grounded)
        {
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            _playerAnim.SetTrigger("Jumping");
        }
    }

    public void GrabLedge(Vector3 pos)
    {
        // should disable controller? "_player.enabled = false;"
        _playerAnim.SetBool("Holding", true);
        transform.position = pos;
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
