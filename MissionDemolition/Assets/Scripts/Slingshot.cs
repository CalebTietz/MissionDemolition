using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{

    public GameObject launchPoint;
    public GameObject projectilePrefab;

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
        if (!aimMode) return;

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
        
        projectile.transform.position = mouse3D;

        if(Input.GetMouseButtonUp(0))
        {
            aimMode = false;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.velocity = -delta * 10f;
            projectile = null;
        }

        if(Input.GetMouseButtonDown(0))
        {
            projectile = Instantiate(projectilePrefab);
            projectile.transform.position = launchPos;
        }
    }

    private void OnMouseEnter()
    {
        if(projectile == null)
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
