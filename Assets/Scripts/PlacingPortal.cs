using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingPortal : MonoBehaviour, IInputClickHandler
{

    public GameObject PortalManager;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if(PortalManager != null)
            PortalManager.SendMessage("PlacePortal");

    }
}