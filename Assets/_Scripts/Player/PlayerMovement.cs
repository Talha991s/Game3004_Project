using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // running speed
    public float runVel;
    // jumping power
    public float jumpVel;

    // joystic object
    public Joystick joystick;
    public Vector2 sensitivity;
   
    //New Addition in hopes of making the character turn pls help- Amber
    Vector3 move;
    //  ^   ^    ^
    /// Events
    void Start()
    {
        
    }


    void Update()
    {
        Movement();
    }

    void Movement()
    {
        //New Addition in hopes of making the character turn pls help- Amber
        move = new Vector3(transform.localPosition.x, 0, transform.localPosition.y).normalized;
        //  ^   ^    ^
        //movement
        if (joystick.Horizontal > sensitivity.x)
        {
            transform.position += new Vector3(runVel * Time.deltaTime, 0, 0);
        }
        else if (joystick.Horizontal < -sensitivity.x)
        {
            transform.position -= new Vector3(runVel * Time.deltaTime, 0, 0);
        }

        if (joystick.Vertical > sensitivity.y)
        {
            transform.position += new Vector3(0, 0, runVel * Time.deltaTime);
        }
        else if (joystick.Vertical < -sensitivity.y)
        {
            transform.position -= new Vector3(0, 0, runVel * Time.deltaTime);
        }
        

    }


}
