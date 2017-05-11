using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCommands : MonoBehaviour
{

    private float delay;

    public bool graviy = true;


    void Start()
    {

        delay = 5.0f;
		Physics.IgnoreLayerCollision(9, 31);
        Debug.Log(Physics.GetIgnoreLayerCollision(9, 31));
    }


    void Update()
    {

        if (!this.GetComponent<Rigidbody>())
        {
            if (delay < 0)
            {
                var rigidbody = this.gameObject.AddComponent<Rigidbody>();
                rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
                
                Physics.gravity = new Vector3(0, -0.8f, 0);
               

                if (!graviy)
                {
                    rigidbody.useGravity = false;
                }
            }
            delay -= Time.deltaTime;

        }

        //Debug.Log(delay);


    }

    public void setMaterial()
    {
        Material mat = new Material(Shader.Find("Pinball/Ball"));
        GetComponent<Renderer>().material = mat;
        
    }



}
