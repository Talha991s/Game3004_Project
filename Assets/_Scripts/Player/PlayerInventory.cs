/*  Author: Tyler McMillan
 *  Date Created: February 3, 2021
 *  Last Updated: February 3, 2021
 *  Description: This script is used for managing everything related to the players inventory. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    private int playerSeeds = 0; //how many seeds the player has collected in the level
    [SerializeField] private Text playerSeedTxt; //reference to the player seed textbox
    private int totalSeeds = 0; //how many seed total in the entire level
    [SerializeField] private Text totalSeedTxt; //reference to the total seed textbox
    [SerializeField] private Animator seedTextAnimator; //reference to the animator to make seed count appear and disappear
    private bool showSeedText = false; //are you showing the seed count
    [SerializeField] private float seedShowTime = 5f; //How long to show the seed count for (seconds)
    private float scount = 0f; //Counter just to hold the amount of time elapsed since opened

    //Count how many seeds are in the map and add them to variable when script awakes
    private void Awake(){
        totalSeeds = GameObject.FindGameObjectsWithTag("Seed").Length;
        totalSeedTxt.text = totalSeeds.ToString();
    }
    void FixedUpdate(){
        //Counter to close players seeds collected when opened
        scount += Time.deltaTime;
        if(showSeedText){
            if(scount > seedShowTime){
                showSeedText = false;
                seedTextAnimator.SetBool("ShowCount", false);
            }
        }
        
    }
    public void OpenInvetory() //Button calls this function to open the inventory
    {
        gameObject.transform.SetAsLastSibling(); //Makes sure the inventory is in front of all other UI.
        gameObject.GetComponent<Animator>().SetBool("OpenInventory", true); //Change bool in animator to true so it opens
        seedTextAnimator.SetBool("ShowCount", true);
    }
     public void CloseInvetory() //Middle button in inventory calls this funtion to close the inventory
    {
        
        gameObject.GetComponent<Animator>().SetBool("OpenInventory", false); //Change bool in animator to false so it closes
        seedTextAnimator.SetBool("ShowCount", false);
        showSeedText = false;
    }
    public void CollectSeed(int _amount){
        //Gets called from "seed script" when player steps in a seeds trigger box, it gets sent that seeds value
        //that then is added to the players seed count, displayed, and resets counter to 0 for when the text disappears.
        playerSeeds += _amount;
        playerSeedTxt.text = playerSeeds.ToString();
        seedTextAnimator.SetBool("ShowCount", true);
        showSeedText = true;
        scount = 0f;
    }
}

