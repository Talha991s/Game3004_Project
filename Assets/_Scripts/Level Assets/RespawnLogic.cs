using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnLogic : MonoBehaviour
{
    [SerializeField] GameObject currentSpawnPoint;
    GameObject oldSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            transform.position = currentSpawnPoint.transform.position;
            transform.rotation = currentSpawnPoint.transform.rotation;
        }

        if (other.gameObject.CompareTag("Spawn"))
        {
            oldSpawnPoint = currentSpawnPoint;
            currentSpawnPoint = other.gameObject;
            oldSpawnPoint = null;
        }
    }
}
