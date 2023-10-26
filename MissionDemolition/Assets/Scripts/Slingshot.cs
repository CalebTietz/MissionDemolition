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
        double distance = Math.Pow(Math.Pow(delta.x, 2) + Math.Pow(delta.y, 2), 0.5);
        
        if(distance < 0.5f)
        {
            projectile = Instantiate(projectilePrefab);
        }
    }

    private void OnMouseDown()
    {
        aimMode = true;
    }

    private void OnMouseUp()
    {
        aimMode = false;
    }
}
