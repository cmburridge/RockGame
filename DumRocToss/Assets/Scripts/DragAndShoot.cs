using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndShoot : MonoBehaviour
{
    //Youtube Tutorial by Muddy Wolf
    
    public float power = 10f;
    public Rigidbody2D rb;

    public Vector2 minPower;
    public Vector2 maxPower;

    private Camera cam;
    private Vector2 force;
    private Vector3 startPoint;
    private Vector3 endPoint;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = Input.mousePosition;
            Debug.Log(startPoint);
        }
    }
}
