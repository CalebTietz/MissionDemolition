using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private static GameObject POI;
    public float easing = 0.5f;
    public Vector2 minXY = Vector2.zero;

    float camZ;
    float camOrthoSize;

    void Awake()
    {
        camZ = this.transform.position.z;
        camOrthoSize = Camera.main.orthographicSize;
    }

    void FixedUpdate()
    {

        Vector3 dest = Vector3.zero;

        if(POI != null)
        {
            Rigidbody rb = POI.GetComponent<Rigidbody>();
            if ((rb != null && rb.IsSleeping()) || Input.GetKey(KeyCode.R))
            {
                POI = null ;
            }
        }

        if (POI != null)
        {
            dest = POI.transform.position;
        }

        dest.x = Mathf.Max(minXY.x, dest.x);
        dest.y = Mathf.Max(minXY.y, dest.y);
        dest = Vector3.Lerp(transform.position, dest, easing);
        dest.z = camZ;

        transform.position = dest;

        Camera.main.orthographicSize = dest.y + camOrthoSize;
    }

    public static GameObject getPOI()
    {
        return POI;
    }
    
    public static void setPOI(GameObject poi)
    {
        POI = poi;
    }
}
