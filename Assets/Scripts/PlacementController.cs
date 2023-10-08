using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

// this automatically adds an ar raycast component
[RequireComponent(typeof(ARRaycastManager))]

public class PlacementController : MonoBehaviour
{

    [SerializeField]
    private GameObject placePrefab;

    public GameObject PlacePrefab
    {
        get{
            return placePrefab;
        }
        set{
            placePrefab  = value;
        }
    }

    private ARRaycastManager arRaycastManager;
    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition (out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition= Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }


    void Update()
    {
        if(!TryGetTouchPosition(out Vector2  touchPosition))
        return;

        if(arRaycastManager.Raycast(touchPosition ,hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            Instantiate(placePrefab,hitPose.position, hitPose.rotation); 
        }
    }

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
}
