using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    PlayerMovement player;

    enum PlatformType
    {
        BASIC,      // No movement
        MOVE,       // Simple movement
        ELEVATOR,   // Simple movement + brief pause at each point (currently unused)
        FLIP,       // Flips over after a pause
    };

    [SerializeField] PlatformType platformType;

    /// Used for MOVE and ELEVATOR
    // positionOne is set to where it is placed in the world
    // positionTwoV is the vector derived from positionTwoT's transform, which is set and changed in Unity
    Vector3 positionOne;
    Vector3 positionTwoV;
    [SerializeField] Transform positionTwoT;

    // Travel speed for the platform
    [SerializeField] float moveSpeed = 1.0f;

    /// Used for FLIP
    [SerializeField] Transform rotationOne;
    [SerializeField] Transform rotationTwo;

    [SerializeField] float rotateSpeed = 1.0f;
    [SerializeField] float flipStart = 0.0f;
    bool hold = true;
    float flipTimer = 2.5f;
    float holdPositionTimer = 5.0f;
    bool move = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();

        if (platformType == PlatformType.MOVE || platformType == PlatformType.ELEVATOR)
        {
            positionOne = transform.position;
            positionTwoV = positionTwoT.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        if (platformType == PlatformType.MOVE || platformType == PlatformType.ELEVATOR)
        {
            // Interpolate position between both, PingPong continues so long as first value keeps going, Time.time is perfect for it
            gameObject.transform.position = Vector3.Lerp(positionOne, positionTwoV, Mathf.PingPong(Time.time * moveSpeed, 1.0f));
        }
    }

    void Rotate()
    {
        if (platformType == PlatformType.FLIP)
        {
            transform.rotation = Quaternion.Lerp(rotationOne.rotation, rotationTwo.rotation, Mathf.PingPong(flipStart + Time.time * rotateSpeed, 1.0f));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(gameObject.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
