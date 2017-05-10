using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteringPortal : MonoBehaviour {

    public PortalManager manager;

    private float Timer;

    private bool portalActive;

    private void Awake()
    {
        Timer = 0;
        portalActive = true;
    }

    void OnCollisionEnter(Collision col)
    {
        if (manager != null && col.gameObject.name == "Cube" && portalActive)
        {

            manager.SendMessage("Entering", this.transform.parent.gameObject);
            Debug.Log("Start entering");
        }
       
    }





    public void startTimer(float wait)
    {     
        portalActive = false;
        Timer = wait;      
        
    }
    private void Update()
    {
        if (!portalActive)
        {
            if(Timer > 0)
            {
                //Debug.Log(Timer);
                Timer -= Time.deltaTime;
            }
            else
            {
                portalActive = true;
            }
        }
    }

}
