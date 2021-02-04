using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // running speed
    public float runVel;
    // jumping power
    public float jumpForce;

    // joystic object
    public Joystick joystick;
    public float sensitivity;

    public Rigidbody rb;
    public bool grounded;

    public CapsuleCollider body;
    public BoxCollider groundCheck;
    
    Vector3 direction;

    public Camera cam;

    /// Events
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        body = GetComponent<CapsuleCollider>();
        groundCheck = transform.GetComponentInChildren<BoxCollider>();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;

        if (joystick.Direction.magnitude > sensitivity)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, angle + 180, 0));

            Vector3 moveDir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            transform.position += moveDir.normalized * runVel * Time.deltaTime;
            
        }
    }

    public void Jump()
    {
        if (grounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        grounded = true;
    }
    private void OnCollisionExit(Collision other)
    {
        grounded = false;
    }
}
