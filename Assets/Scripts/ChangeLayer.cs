using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayer : MonoBehaviour {



    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "Cube")
        {
            Debug.Log("Enter");
            col.gameObject.layer = 9;
        }

    }

    private void OnTriggerExit(Collider col)
    {

        if (col.gameObject.name == "Cube")
        {
            Debug.Log("Exit");
            col.gameObject.layer = 0;
        }
    }

    

}
