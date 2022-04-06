using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movePower;
    [SerializeField] float jumpPower;
    float xInput;
    [SerializeField] Camera cam;
    [SerializeField] float cameraDepth;
    Rigidbody rb;

    bool jumping;
    bool grounded;

    void Start()
    {
        rb = GetComponent < Rigidbody>();
    }

    void Update()
    {
        GetInputs();
    }

    private void FixedUpdate()
    {
        PhysicsMovement();
    }

    private void LateUpdate()
    {
        CameraFollow();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, 2f))
        {
            grounded = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Physics.Raycast(transform.position, Vector3.down, 2f))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }

    void GetInputs()
    {
        xInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jumping = true;
        }
    }

    void PhysicsMovement()
    {
        Vector3 movementVelocity = new Vector3(xInput * movePower, rb.velocity.y, 0);
        rb.velocity = Vector3.Lerp(rb.velocity, movementVelocity, Time.fixedDeltaTime * 1f);

        if (jumping && grounded)
        {
            jumping = false;
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    void CameraFollow()
    {
        Vector3 followPosition = new Vector3(transform.position.x + (xInput * 5f), transform.position.y, -cameraDepth);
        cam.transform.position = Vector3.Slerp(cam.transform.position, followPosition, Time.deltaTime);
    }
}
