/*  Author: Joseph Malibiran
 *  Date Created: January 29, 2021
 *  Last Updated: January 30, 2021
 *  Usage: Drag and drop the prefab this script is attached to into the Canvas as a child. You can also just drop this prefab anywhere in the scene and it will automatically find its rightful position within Canvas.
 *  Description: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapScr : MonoBehaviour
{
    [Header("Manualset References")]
    [SerializeField] private Transform targetPlayerRef;             //The player chracter that the minimap will follow and center on
    
    [Header("Preset References")]
    [SerializeField] private GameObject canvasContainerRef;         //Drag and Drop canvas gameobject here
    [SerializeField] private Transform miniMapCamContainerRef;      //Container object that holds the camera for the minimap view

    [Header("Minimap Settings")]
    [SerializeField] private float camFollowSpeed = 10;
    //[SerializeField] private bool bUseCircleMask;

    private void Awake() 
    {
        InsureCanvasExists();               //Insures that this object is within Canvas 
        ExtractCameraToRootHierarchy();     //Extract the Minimap Camera from within the prefab and unto the root of the scene hierarchy.
    }

    private void Update() 
    {
        FollowTargetPlayerWithCam();        //Allows the minimap camera to follow the player movements. 
    }

    //Insures that this object is within Canvas 
    private void InsureCanvasExists() {

        //Check if this object is already in its proper place- as a child of Canvas object. Note: MinimapScr.cs is supposed to be attached to a prefab that is meant to go in the Canvas
        if (this.transform.parent) 
        {
            if (this.transform.parent.GetComponent<Canvas>()) {
                return;
            }
        }

        //Check if Canvas exists with the name 'Canvas'
        if (GameObject.Find("Canvas")) 
        {
            if (GameObject.Find("Canvas").GetComponent<Canvas>()) 
            {
                canvasContainerRef = GameObject.Find("Canvas");
                this.transform.SetParent(canvasContainerRef.transform);
                this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-128,-128);
                return;
            }
        }

        //Check if Canvas exists with the name 'HUD'
        if (GameObject.Find("HUD"))
        {
            if (GameObject.Find("HUD").GetComponent<Canvas>()) 
            {
                canvasContainerRef = GameObject.Find("HUD");
                this.transform.SetParent(canvasContainerRef.transform);
                this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-128,-128);
                return;
            }
        }

        //If Canvas object cannot be found: build one
        canvasContainerRef = new GameObject();
        canvasContainerRef.gameObject.AddComponent<Canvas>();
        canvasContainerRef.gameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        canvasContainerRef.gameObject.AddComponent<CanvasScaler>();
        canvasContainerRef.gameObject.AddComponent<GraphicRaycaster>();
        canvasContainerRef.name = "Canvas";
        this.transform.SetParent(canvasContainerRef.transform);
        this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-128,-128);
    }

    //Extract the Minimap Camera from within the prefab and unto the root of the scene hierarchy.
    private void ExtractCameraToRootHierarchy() 
    {
        if (!miniMapCamContainerRef) 
        {
            Debug.LogError("[Error] miniMapCamContainerRef missing! Aborting operation...");
            return;
        }

        miniMapCamContainerRef.SetParent(null);

        if (targetPlayerRef) {
            miniMapCamContainerRef.position = new Vector3(targetPlayerRef.transform.position.x, targetPlayerRef.transform.position.y + 10, targetPlayerRef.transform.position.z);
        }
    }

    //Allows the minimap camera to follow the player movements. 
    //Note: This is elected over simply having the camera as a child of the target player character to avoid potential issues of this object being destroyed along with the player character.
    private void FollowTargetPlayerWithCam() 
    {
        if (!targetPlayerRef) 
        {
            return;
        }

        if (!miniMapCamContainerRef) 
        {
            Debug.LogError("[Error] miniMapCamContainerRef missing! Aborting operation...");
            return;
        }

        miniMapCamContainerRef.position = Vector3.MoveTowards(miniMapCamContainerRef.position, new Vector3(targetPlayerRef.position.x, targetPlayerRef.position.y + 10, targetPlayerRef.position.z), camFollowSpeed * Time.deltaTime);
    }

}
