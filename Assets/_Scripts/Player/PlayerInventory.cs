/*  Author: Tyler McMillan
 *  Date Created: February 3, 2021
 *  Last Updated: February 3, 2021
 *  Description: This script is used for managing everything related to the players inventory. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //public GameObject InventoryObj;  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OpenInvetory() //Button calls this function to open the inventory
    {
        gameObject.transform.SetAsLastSibling(); //Makes sure the inventory is in front of all other UI.
        gameObject.GetComponent<Animator>().SetBool("OpenInventory", true); //Change bool in animator to true so it opens
    }
     public void CloseInvetory() //Middle button in inventory calls this funtion to close the inventory
    {
        
        gameObject.GetComponent<Animator>().SetBool("OpenInventory", false); //Change bool in animator to false so it closes
    }
}
