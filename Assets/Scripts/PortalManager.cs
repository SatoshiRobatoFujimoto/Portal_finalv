using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HoloToolkit.Unity.SpatialMapping;

public class PortalManager : MonoBehaviour
{
    public enum PlacementSurfaces
    {
        // Horizontal surface with an upward pointing normal.    
        Horizontal = 1,

        // Vertical surface with a normal facing the user.
        Vertical = 2,
    }
    private float maximumPlacementDistance = 5.0f;

    private float distanceThreshold = 0.05f; //0.05f;

    private Vector3 lookPosition;
    private Vector3 surfaceNormal;

    public GameObject GreenPortal;
    public GameObject RedPortal;

    private Vector3 GreenPosition;
    private Vector3 RedPosition;
    private Vector3 GreenNormal;
    private Vector3 RedNormal;

    public GameObject Cube;

    private bool RedActivated;
    private bool GreenActivated;

    public GameObject SpaceChecker;

    //Az amelyiket elhelyezhetjük most!
    private GameObject ActivePortal;
    

    // Use this for initialization
    void Start()
    {
        GreenPortal.SetActive(false);
        RedPortal.SetActive(false);

        ActivePortal = GreenPortal;

    }



    public void PlacePortal()
    {
        
        Vector3 GazeHitPosition = GazeManager.Instance.HitInfo.point;
        surfaceNormal = GazeManager.Instance.HitInfo.normal;



        if(SpaceChecker.GetComponent<ValidatePlacement>().isEnoughtPlace(GazeHitPosition, surfaceNormal))
        {
            ActivePortal.transform.position = GazeHitPosition;// + (surfaceNormal * 0.01f);

            ActivePortal.transform.rotation = Quaternion.LookRotation(surfaceNormal);

            if(!ActivePortal.active)
                ActivePortal.SetActive(true);

            if (ActivePortal == GreenPortal)
            {
                GreenPosition = GazeHitPosition;
                GreenNormal = surfaceNormal;
                ActivePortal = RedPortal;
                GreenActivated = true;
            }
            else
            {
                RedPosition = GazeHitPosition;
                RedNormal = surfaceNormal;
                ActivePortal = GreenPortal;
                RedActivated = true;
            }
        }

    }

   

    public void Entering(GameObject InPortal)
    {
       
        Vector3 v = Cube.GetComponent<Rigidbody>().velocity;
        
        if (InPortal == GreenPortal && RedActivated)
        {
            Cube.transform.position = RedPosition;

            Cube.GetComponent<Rigidbody>().AddForce((Quaternion.Euler(RedNormal) * v));
            Cube.GetComponent<Rigidbody>().AddForce(RedNormal*100);
            Debug.Log(Quaternion.LookRotation(RedNormal) * v);
            
        }
        else if(InPortal == RedPortal && GreenActivated)
        {
            Cube.transform.position = GreenPosition;// + (GreenNormal * 0.5f);

            
            Cube.GetComponent<Rigidbody>().AddForce((Quaternion.Euler(GreenNormal) * v));
            Cube.GetComponent<Rigidbody>().AddForce(GreenNormal * 100);
            Debug.Log(Quaternion.LookRotation(GreenNormal) * v);
        }
        Debug.Log("k");
        //GreenPortal.GetComponent<EnteringPortal>().startTimer(0.1f);
       // RedPortal.GetComponent<EnteringPortal>().startTimer(0.1f);

    }

}
