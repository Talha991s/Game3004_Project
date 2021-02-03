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
        
        //movement
        if (joystick.Horizontal > sensitivity)
        {
            transform.position += new Vector3(runVel * Time.deltaTime, 0, 0);
        }
        else if (joystick.Horizontal < -sensitivity)
        {
            transform.position -= new Vector3(runVel * Time.deltaTime, 0, 0);
        }

        if (joystick.Vertical > sensitivity)
        {
            transform.position += new Vector3(0, 0, runVel * Time.deltaTime);
        }
        else if (joystick.Vertical < -sensitivity)
        {
            transform.position -= new Vector3(0, 0, runVel * Time.deltaTime);
        }
        
        if(joystick.Direction.magnitude > sensitivity)
        {
            float angle = Mathf.Atan2(joystick.Direction.x, joystick.Direction.y) *  Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle + 180, 0);
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
