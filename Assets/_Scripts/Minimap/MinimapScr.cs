/*  Author: Joseph Malibiran
 *  Date Created: January 29, 2021
 *  Last Updated: January 30, 2021
 *  Usage: Drag and drop the prefab this script is attached to into the Canvas as a child. You can also just drop this prefab anywhere in the scene and it will automatically find its rightful position within Canvas.
 *  Then use SetTargetPlayer() to add a player chracter object that the minimap camera will follow. Turn off the Minimap Marker layer on other cameras except the Minimap camera.
 *  Description: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapScr : MonoBehaviour
{
    [Header("Manualset References")]                                //References that have to be manually set beyond the prefab- Either by drag and drop or calling a function that assigns it.
    [SerializeField] private Transform targetPlayerRef;             //The player chracter that the minimap will follow and center on.
    
    [Header("Preset References")]                                   //References that the prefab should already have OR that the script will automatically find.
    [SerializeField] private GameObject canvasContainerRef;         //Drag and Drop canvas gameobject here.
    [SerializeField] private GameObject minimapMaskRef;             //Image mask used to shape the minimap
    [SerializeField] private Transform miniMapCamContainerRef;      //Container object that holds the camera for the minimap view.
    [SerializeField] private Material playerMinimapMarkerRef;       //The visual marker of how a player chracter will appear on the minimap.
    [SerializeField] private Material enemyMinimapMarkerRef;        //The visual marker of how a player chracter will appear on the minimap.

    [Header("Minimap Settings")]
    [SerializeField] private float camFollowSpeed = 10;             //How fast the minimap camera will follow the player. Note: this does not utilize smooth interpolation.
    [SerializeField] private float miniMapSize = 256;               //How big the minimap will appear in the screen.
    [SerializeField] private float miniMapZoom = 26;                //How much ground the minimap can cover. It's like an aerial view zoom effect.
    [SerializeField] private float miniMapIconSizes = 6;
    //[SerializeField] private bool bUseCircleMask;

    private Transform initialPlayerRef;                             //Used to compare with targetPlayerRef to check if targetPlayerRef has been changed.
    private Transform initialPlayerIconRef;

    //Advanced Settings
    private float camOverheadDistance = 30;
    private float iconOverheadHeight = 20;

    private void Awake() 
    {
        InsureCanvasExists();               //Insures that this object is within Canvas.
        ExtractCameraToRootHierarchy();     //Extract the Minimap Camera from within the prefab and unto the root of the scene hierarchy.
    }

    private void Update() 
    {
        FollowTargetPlayerWithCam();        //Allows the minimap camera to follow the player movements. 

        //If the target player character has been changed: Create new Minimap icon for the new player character.
        if (HasTargetPlayerChanged()) 
        {
            //Destroy previous player icon.
            if (initialPlayerIconRef) 
            {
                Destroy(initialPlayerIconRef);
            }
            //Create new Minimap icon for the new player character.
            AddMinimapMarker(targetPlayerRef, MinimapMarker.PLAYER);
        }
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
                this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(miniMapSize, miniMapSize);
                this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(miniMapSize * -0.5f, miniMapSize * -0.5f);
                minimapMaskRef.GetComponent<RectTransform>().sizeDelta = new Vector2(miniMapSize, miniMapSize);
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
                this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(miniMapSize, miniMapSize);
                this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(miniMapSize * -0.5f, miniMapSize * -0.5f);
                minimapMaskRef.GetComponent<RectTransform>().sizeDelta = new Vector2(miniMapSize, miniMapSize);
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
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(miniMapSize, miniMapSize);
        this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(miniMapSize * -0.5f, miniMapSize * -0.5f);
        minimapMaskRef.GetComponent<RectTransform>().sizeDelta = new Vector2(miniMapSize, miniMapSize);
        initialPlayerIconRef = canvasContainerRef.transform;
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

        if (miniMapCamContainerRef.GetChild(0).GetComponent<Camera>()) 
        {
            miniMapCamContainerRef.GetChild(0).GetComponent<Camera>().orthographicSize = miniMapZoom;
        }
        else 
        {
            Debug.LogError("[Error] Could not find Minimap Camera!");
        }

        if (targetPlayerRef) 
        {
            miniMapCamContainerRef.position = new Vector3(targetPlayerRef.transform.position.x, targetPlayerRef.transform.position.y + camOverheadDistance, targetPlayerRef.transform.position.z);
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

        //If Minimap Camera is too far from the player: teleport the camera to the player.
        //TODO remove. This part is only applicable in development stages; built games cannot drag and drop into the inspector during runtime.
        if (Vector2.Distance(new Vector2(miniMapCamContainerRef.position.x, miniMapCamContainerRef.position.z), new Vector2(targetPlayerRef.position.x, targetPlayerRef.position.z)) > 10 ||
            miniMapCamContainerRef.position.y < targetPlayerRef.position.y ||
            Vector3.Distance(miniMapCamContainerRef.position, targetPlayerRef.position) > (10 + camOverheadDistance)) 
        {
            miniMapCamContainerRef.position = new Vector3(targetPlayerRef.position.x, targetPlayerRef.position.y + camOverheadDistance, targetPlayerRef.position.z);
            return;
        }

        //Set minimap Camera to follow the player
        miniMapCamContainerRef.position = Vector3.MoveTowards(miniMapCamContainerRef.position, new Vector3(targetPlayerRef.position.x, targetPlayerRef.position.y + camOverheadDistance, targetPlayerRef.position.z), camFollowSpeed * Time.deltaTime);
        
    }

    //Checks if the targetPlayerRef has changed since last checked.
    private bool HasTargetPlayerChanged() 
    {
        if (targetPlayerRef == initialPlayerRef) 
        {
            return false;
        }

        initialPlayerRef = targetPlayerRef;
        return true;
    }

    //Creates an icon that will represent the given target object on the minimap
    //Note: This marker object should only be seen by the minimap camera; turn off the Minimap Marker layer on other cameras.
    private void AddMinimapMarker(Transform targetObj, MinimapMarker markerType) 
    {
        GameObject minimapMarker; 

        //Temp marker type check. Will need to be modified as more marker types are accomodated.
        if ( !(markerType == MinimapMarker.PLAYER || markerType == MinimapMarker.ENEMY) ) 
        {
            Debug.LogError("[Error] Invalid minimap marker type; Aborting operation...");
            return;
        }

        minimapMarker = GameObject.CreatePrimitive(PrimitiveType.Quad);
        minimapMarker.name = "Minimap Icon";
        minimapMarker.layer = LayerMask.NameToLayer("Minimap Marker");
        minimapMarker.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        minimapMarker.GetComponent<MeshRenderer>().receiveShadows = false;
        Destroy(minimapMarker.GetComponent<MeshCollider>());

        minimapMarker.transform.SetParent(targetObj);
        minimapMarker.transform.localPosition = new Vector3(0, iconOverheadHeight, 0);
        minimapMarker.transform.localEulerAngles = new Vector3(90, 0, 0);
        minimapMarker.transform.localScale = new Vector3(miniMapIconSizes, miniMapIconSizes, 1);

        switch (markerType) 
        {
            case MinimapMarker.NONE:
                return;
            case MinimapMarker.PLAYER:
                if (playerMinimapMarkerRef) 
                {
                    minimapMarker.GetComponent<MeshRenderer>().material = playerMinimapMarkerRef;
                }
                else 
                {
                    Debug.LogError("[Error] Player minimap marker material reference missing!");
                }
                break;
            case MinimapMarker.ENEMY:
                if (enemyMinimapMarkerRef) 
                {
                    minimapMarker.GetComponent<MeshRenderer>().material = enemyMinimapMarkerRef;
                }
                else 
                {
                    Debug.LogError("[Error] Enemy minimap marker material reference missing!");
                }
                break;
            default:
                return;
        }

    }

    //Set new player character that the minimap cam will follow.
    public void SetTargetPlayer(Transform insertPlayer) 
    {
        targetPlayerRef = insertPlayer;
        miniMapCamContainerRef.position = new Vector3(targetPlayerRef.transform.position.x, targetPlayerRef.transform.position.y + 10, targetPlayerRef.transform.position.z);

        //If the target player character has been changed: Create new Minimap icon for the new player character.
        if (HasTargetPlayerChanged()) 
        {
            AddMinimapMarker(targetPlayerRef, MinimapMarker.PLAYER);
        }

        miniMapCamContainerRef.position = new Vector3(targetPlayerRef.transform.position.x, targetPlayerRef.transform.position.y + 30, targetPlayerRef.transform.position.z);
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

    //Set whether or not the minimap is visible.
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

    //Adjust how big the minimap will appear in the screen.
    public void SetMinimapSize(float newMiniMapSize) 
    {
        if (!minimapMaskRef) 
        {
            Debug.LogError("[Error] minimapMaskRef missing! Aborting operation...");
            return;
        }

        if (newMiniMapSize <= 0) 
        {
            Debug.LogError("[Error] Invalid minimap size! Aborting operation...");
            return;
        }

        miniMapSize = newMiniMapSize;
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(miniMapSize, miniMapSize);
        this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(miniMapSize * -0.5f, miniMapSize * -0.5f);
        minimapMaskRef.GetComponent<RectTransform>().sizeDelta = new Vector2(miniMapSize, miniMapSize);
    }

    //Adjust how much ground the minimap can cover. Like an aerial view zoom effect.
    public void SetMinimapZoom(float newZoomAmount) 
    {
        if (!miniMapCamContainerRef) 
        {
            Debug.LogError("[Error] miniMapCamContainerRef missing! Aborting operation...");
            return;
        }

        if (!miniMapCamContainerRef.GetChild(0).GetComponent<Camera>()) 
        {
            Debug.LogError("[Error] Could not find Minimap Camera! Aborting operation...");
            return;
        }

        if (newZoomAmount <= 0) 
        {
            Debug.LogError("[Error] Invalid camera zoom amount! Aborting operation...");
            return;
        }

        miniMapZoom = newZoomAmount;
        miniMapCamContainerRef.GetChild(0).GetComponent<Camera>().orthographicSize = miniMapZoom;
    }

    //Adjust the size of icons in the minimap
    public void SetIconSize(float newMiniMapIconSize) 
    {
        if (!initialPlayerIconRef) 
        {
            return;
        }

        if (newMiniMapIconSize <= 0) 
        {
            Debug.LogError("[Error] Invalid icon size! Aborting operation...");
            return;
        }

        miniMapIconSizes = newMiniMapIconSize;
        initialPlayerIconRef.localScale = new Vector3(miniMapIconSizes, miniMapIconSizes, 1);

        //TODO adjust other icons here
    }

}
