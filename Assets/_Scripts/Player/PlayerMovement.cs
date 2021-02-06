using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // running speed
    public float speed;
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
    float hor;
    float vert;
    public float turnSmoothVelocity;
    public float turnSmoothTime;
    Animator animator;
    

    public Camera cam;

    public enum ControlSettings
    {
        DESKTOP,
        MOBILE
    }

    public ControlSettings controlSettings;
    public bool inventory;


    /// Events
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        body = GetComponent<CapsuleCollider>();
        groundCheck = transform.GetComponentInChildren<BoxCollider>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (controlSettings == ControlSettings.DESKTOP)
        {
            Controls();
        }
        else if (controlSettings == ControlSettings.MOBILE)
        {
            ControlsMobile();
        }
       
    }

    void Controls()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (!inventory)
            {
                GameObject.Find("Inventory").GetComponent<PlayerInventory>().OpenInvetory();
                inventory = true;
            }
            else
            {
                GameObject.Find("Inventory").GetComponent<PlayerInventory>().CloseInvetory();
                inventory = false;
            }

        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Side to Side movement
        // Flipped the hor values that don't seem to work with new model.
        if (Input.GetKey(KeyCode.A))
        {
            hor = 1;
            
            animator.SetBool("Moving", true);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            hor = -1;
            animator.SetBool("Moving", true);
        }
        else
        {
            hor = 0;
            
        }
        // Forward and Back movement
        // Flipped the vert values that don't seem to work with new model.
        if(Input.GetKey(KeyCode.W))
        {
            vert = -1;
            animator.SetBool("Moving", true);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            vert = 1;
            animator.SetBool("Moving", true);
        }
        else
        {
            vert = 0;
            animator.SetBool("Moving", false);
        }

        direction = new Vector3(hor, 0, vert);

        if(direction.magnitude > sensitivity)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,turnSmoothTime);

            transform.rotation = Quaternion.Euler(0, angle + 180, 0);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            transform.position += moveDir * speed * Time.deltaTime;
        }
    }

    void ControlsMobile()
    {
        direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;

        if (joystick.Direction.magnitude > sensitivity)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, angle + 180, 0));

            Vector3 moveDir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            transform.position += moveDir.normalized * speed * Time.deltaTime;
            
        }
    }

    public void Jump()
    {
        if (grounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
            animator.SetBool("Jumping", true);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        grounded = true;
        animator.SetBool("Jumping", false);
    }
    private void OnCollisionExit(Collision other)
    {
        grounded = false;
    }
}
