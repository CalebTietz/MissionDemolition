using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{

    public GameObject launchPoint;
    public GameObject projectilePrefab;
    public GameObject projLinePrefab;
    public float slingShotStrength = 15f;

    private bool aimMode = false;
    private GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        launchPoint.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        if (!aimMode || FollowCam.getPOI() != null) return;

        Vector3 mouse2D = Input.mousePosition;
        mouse2D.z = -Camera.main.transform.position.z;
        Vector3 mouse3D = Camera.main.ScreenToWorldPoint(mouse2D);

        Vector3 launchPos = launchPoint.transform.position;
        
        Vector3 delta = launchPos - mouse3D;

        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if(delta.magnitude > maxMagnitude)
        {
            delta.Normalize();
            delta *= maxMagnitude;
        }
        

        if(Input.GetMouseButtonDown(0))
        {
            projectile = Instantiate(projectilePrefab);
            projectile.transform.position = launchPos;
        }
        
        projectile.transform.position = -delta + launchPos;

        if(Input.GetMouseButtonUp(0))
        {
            aimMode = false;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.velocity = delta * slingShotStrength;
            FollowCam.setPOI(projectile);
            Instantiate(projLinePrefab, projectile.transform);
            projectile = null;
            MissionDemolition.SHOT_FIRED();
        }

    }

    private void OnMouseOver()
    {
        if(projectile == null && FollowCam.getPOI() == null)
        {
            launchPoint.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        launchPoint.SetActive(false);
    }

    private void OnMouseDown()
    {
        aimMode = true;
        launchPoint.SetActive(false);
    }
}
