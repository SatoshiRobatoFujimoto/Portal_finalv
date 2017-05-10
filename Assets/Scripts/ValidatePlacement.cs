using HoloToolkit.Unity.SpatialMapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidatePlacement : MonoBehaviour {


    private float distanceThreshold = 0.2f;

    private float maximumPlacementDistance = 5.0f;

    private BoxCollider boxCollider = null;

    List<Vector3> points = new List<Vector3>();
    Vector3 dir;

    AudioSource audioSource;
    AudioClip portalFailed;

    private void Awake()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        audioSource = gameObject.GetComponent<AudioSource>();
        boxCollider.enabled = false;
    }


    public bool isEnoughtPlace(Vector3 position, Vector3 normal)
    {
        try
        {
            boxCollider.enabled = true;
            Vector3 raycastDirection = -normal;

            Vector3 originPosition = this.transform.position;
            Quaternion originRotation = this.transform.rotation;

            this.transform.position = position + (normal * 0.1f);
            this.transform.rotation = Quaternion.LookRotation(-normal);

            List<Vector3> facePoints = GetColliderFacePoints(normal);

            //for (int i = 0; i < facePoints.Count; i++)
            //{
            //    facePoints[i] = gameObject.transform.TransformVector(facePoints[i]) + gameObject.transform.position;
            //    Debug.Log("facepoint:" + facePoints[i]);
            //}


            RaycastHit centerHit;



            dir = raycastDirection;



            if (!Physics.Raycast(facePoints[0],
                            raycastDirection,
                            out centerHit,
                            maximumPlacementDistance))
            {

                Debug.Log("Nem volt mert a center nem metszett" + centerHit.point);
                audioSource.Play();
                return false;
            }


            position = centerHit.point;
            normal = centerHit.normal;

            for (int i = 1; i < facePoints.Count; i++)
            {
                RaycastHit hitInfo;

                Debug.DrawRay(facePoints[0], raycastDirection, Color.yellow);

                if (Physics.Raycast(facePoints[i],
                                    raycastDirection,
                                    out hitInfo,
                                    maximumPlacementDistance))
                {

                    if (!IsEquivalentDistance(centerHit.distance, hitInfo.distance))
                    {
                        Debug.Log("Túl nagy volt az elétrés itt " + i);
                        audioSource.Play();
                        return false;
                    }
                }
                else
                {

                    Debug.Log("Nem volt mert a " + i + "pontból nem volt jó. Innen " + facePoints[i]);
                    audioSource.Play();
                    return false;
                }
            }

            Debug.Log("Volt elegendő hely!");
            return true;
        }
        finally
        {
            boxCollider.enabled = false;
        }
    }


    private List<Vector3> GetColliderFacePoints(Vector3 norm)
    {

        Vector3 center = boxCollider.center;
        
        points = new List<Vector3>();


        points.Add(center + gameObject.transform.position);


        //points.Add(thisMatrix.MultiplyPoint3x4(thisMatrix.MultiplyPoint3x4(extents)));
        //points.Add(thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, extents.y, extents.z)));
        //points.Add(thisMatrix.MultiplyPoint3x4(new Vector3(extents.x, extents.y, -extents.z)));
        //points.Add(thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, extents.y, -extents.z)));
        //points.Add(thisMatrix.MultiplyPoint3x4(new Vector3(extents.x, -extents.y, extents.z)));
        //points.Add(thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, -extents.y, extents.z)));
        //points.Add(thisMatrix.MultiplyPoint3x4(new Vector3(extents.x, -extents.y, -extents.z)));
        // points.Add(thisMatrix.MultiplyPoint3x4(thisMatrix.MultiplyPoint3x4(-extents)));

        Vector3 boundPoint1 = boxCollider.bounds.min;
        Vector3 boundPoint2 = boxCollider.bounds.max;


        points.Add(new Vector3(boundPoint1.x, boundPoint1.y, boundPoint2.z));
        points.Add(new Vector3(boundPoint1.x, boundPoint2.y, boundPoint1.z));
        points.Add(new Vector3(boundPoint2.x, boundPoint1.y, boundPoint1.z));
        points.Add(new Vector3(boundPoint1.x, boundPoint2.y, boundPoint2.z));
        points.Add(new Vector3(boundPoint2.x, boundPoint1.y, boundPoint2.z));
        points.Add(new Vector3(boundPoint2.x, boundPoint2.y, boundPoint1.z));
        

       


        return points;
    }



    private bool IsEquivalentDistance(float d1, float d2)
    {
        float dist = Mathf.Abs(d1 - d2);
        return (dist <= distanceThreshold);
    }

    private void Update()
    {

        //Debug.DrawRay(points[0], dir, Color.yellow, 5.0f);
        /*foreach(Vector3 v in points)
        {
            Debug.DrawRay(v, dir, Color.cyan);
        }
        */
    }
}
