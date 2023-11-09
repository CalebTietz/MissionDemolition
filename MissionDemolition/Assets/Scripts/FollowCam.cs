using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private static FollowCam S;
    private static GameObject POI;

    public enum eView
    {
        none,
        slingshot,
        castle,
        both
    };

    public float easing = 0.5f;
    public Vector2 minXY = Vector2.zero;
    public GameObject viewBothGO;

    float camZ;
    float camOrthoSize;
    public eView nextView = eView.slingshot;

    void Awake()
    {
        S = this;
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

    public void SwitchView(eView newView)
    {
        if(newView == eView.none)
        {
            newView = nextView;
        }

        switch(newView)
        {
            case eView.slingshot:
                POI = null;
                nextView = eView.castle;
                break;

            case eView.castle:
                POI = MissionDemolition.GET_CASTLE();
                nextView = eView.both;
                break;

            case eView.both:
                POI = viewBothGO;
                nextView = eView.slingshot;
                break;

        }
    }

    public void SwitchView()
    {
        SwitchView(eView.none);
    }

    public static void SWITCH_VIEW(eView newView)
    {
        S.SwitchView(newView);
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
