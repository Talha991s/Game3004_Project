/*  Author: Joseph Malibiran
 *  Date Created: January 29, 2021
 *  Last Updated: January 30, 2021
 *  Usage: Drag and drop the prefab this script is attached to into the Canvas as a child. You can also just drop this prefab anywhere in the scene and it will automatically find its rightful position within Canvas.
 *  Then use SetTargetPlayer() to add a player chracter object that the minimap camera will follow.
 *  Description: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapScr : MonoBehaviour
{
    [Header("Manualset References")]
    [SerializeField] private Transform targetPlayerRef;             //The player chracter that the minimap will follow and center on.
    
    [Header("Preset References")]
    [SerializeField] private GameObject canvasContainerRef;         //Drag and Drop canvas gameobject here.
    [SerializeField] private GameObject minimapMaskRef;
    [SerializeField] private Transform miniMapCamContainerRef;      //Container object that holds the camera for the minimap view.
    [SerializeField] private Material playerMinimapMarkerRef;       //The visual marker of how a player chracter will appear on the minimap.
    [SerializeField] private Material enemyMinimapMarkerRef;       //The visual marker of how a player chracter will appear on the minimap.

    [Header("Minimap Settings")]
    [SerializeField] private float camFollowSpeed = 10;
    //[SerializeField] private float miniMapSize = 1;
    //[SerializeField] private bool bUseCircleMask;

    

    private void Awake() 
    {
        InsureCanvasExists();               //Insures that this object is within Canvas.
        ExtractCameraToRootHierarchy();     //Extract the Minimap Camera from within the prefab and unto the root of the scene hierarchy.
    }

    private void Update() 
    {
        FollowTargetPlayerWithCam();        //Allows the minimap camera to follow the player movements. 
    }

    //Insures that this object is within Canvas.
    private void InsureCanvasExists() 
    {

        //Check if this object is already in its proper place- as a child of Canvas object. Note: MinimapScr.cs is supposed to be attached to a prefab that is meant to go in the Canvas.
        if (this.transform.parent) 
        {
            if (this.transform.parent.GetComponent<Canvas>()) 
            {
                return;
            }
        }

        //Check if Canvas exists with the name 'Canvas'.
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

        //Check if Canvas exists with the name 'HUD'.
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

        //If Canvas object cannot be found: build one.
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

        if (targetPlayerRef) 
        {
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

    //Untested WIP
    private void AddMinimapMarker(Transform targetObj, int markerType) 
    {
        GameObject minimapMarker; 

        if (markerType <= 0) 
        {
            Debug.LogError("[Error] Invalid minimap marker type; Aborting operation...");
            return;
        }

        minimapMarker = GameObject.CreatePrimitive(PrimitiveType.Quad);
        minimapMarker.name = "Minimap Marker";
        minimapMarker.layer = LayerMask.NameToLayer("Minimap Marker");
        minimapMarker.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        minimapMarker.GetComponent<MeshRenderer>().receiveShadows = false;
        Destroy(minimapMarker.GetComponent<MeshCollider>());

        switch (markerType) 
        {
            case 0:
                return;
            case 1:
                if (playerMinimapMarkerRef) 
                {
                    minimapMarker.GetComponent<MeshRenderer>().material = playerMinimapMarkerRef;
                }
                break;
            default:
                return;
        }

    }

    //Untested WIP
    private void AddMinimapMarker(Transform targetObj, MinimapMarker markerType) 
    {
        GameObject minimapMarker; 

        minimapMarker = GameObject.CreatePrimitive(PrimitiveType.Quad);

    }

    //Set new player character that the minimap cam will follow.
    public void SetTargetPlayer(Transform insertPlayer) 
    {
        targetPlayerRef = insertPlayer;
        miniMapCamContainerRef.position = new Vector3(targetPlayerRef.transform.position.x, targetPlayerRef.transform.position.y + 10, targetPlayerRef.transform.position.z);

        AddMinimapMarker(targetPlayerRef, MinimapMarker.PLAYER);
    }

    //Toggle whether or not the minimap is visible
    public void ToggleMiniMapVisibility() 
    {
        //If minimapMaskRef cannot be found, try to find it. If it still cannot be found return with error log.
        if (!minimapMaskRef) 
        {
            if (this.transform.GetChild(0)) 
            {
                if (this.transform.GetChild(0).gameObject.GetComponent<Mask>()) 
                {
                    minimapMaskRef = this.transform.GetChild(0).gameObject;
                }
            }
            if (!minimapMaskRef)
            { 
                Debug.LogError("[Error] Minimap mask reference missing! Aborting operation...");
                return;
            }
        }

        if (minimapMaskRef.activeSelf) 
        {
            minimapMaskRef.SetActive(false); //Turn off Minimap visual 
        }
        else 
        {
            minimapMaskRef.SetActive(true);  //Turn on Minimap visual
        }
    }

    //Set whether or not the minimap is visible
    public void SetMiniMapVisibility(bool set) 
    {
        //If minimapMaskRef cannot be found, try to find it. If it still cannot be found return with error log.
        if (!minimapMaskRef) 
        {
            if (this.transform.GetChild(0)) 
            {
                if (this.transform.GetChild(0).gameObject.GetComponent<Mask>()) 
                {
                    minimapMaskRef = this.transform.GetChild(0).gameObject;
                }
            }
            if (!minimapMaskRef)
            { 
                Debug.LogError("[Error] Minimap mask reference missing! Aborting operation...");
                return;
            }
        }

        //Set Minimap visual
        minimapMaskRef.SetActive(set);
    }



}
