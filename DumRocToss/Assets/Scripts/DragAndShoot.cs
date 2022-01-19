using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndShoot : MonoBehaviour
{
    //code involves Youtube Tutorial by Muddy Wolf
    
    public float power = 10f;
    public Rigidbody2D rb;

    public Vector2 minPower;
    public Vector2 maxPower;

    private LineBehavior lb;

    private Camera cam;
    private Vector2 force;
    private Vector3 startPoint;
    private Vector3 endPoint;
    
    private bool isGrounded;
    public GameObject GroundCheck;

    private float jumpTimeCounter;
    private bool isJumping;
    public float jumpCount;
    public float jumpCountMax;

    public GameObject indicator;


    private void Start()
    {
        cam = Camera.main;
        lb = GetComponent<LineBehavior>();
    }

    private void Update()
    {
        if (GroundCheck.gameObject.activeInHierarchy)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        
        if (isGrounded == true && jumpCount == 0)
        {
            jumpCount = jumpCountMax;
        }

        if (jumpCount <= 0)
        {
            indicator.SetActive(false);
        }
        else
        {
            indicator.SetActive(true);
        }

        if (jumpCount > 0 && Input.GetMouseButtonDown(0))
        {
            startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 15; 
        }

        if (jumpCount > 0 && Input.GetMouseButton(0))
        {
            Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            currentPoint.z = 15; 
            lb.RenderLine(startPoint, currentPoint);
        }
        
        if (jumpCount > 0 && Input.GetMouseButtonUp(0))
        {
            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 15;

            force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
            rb.AddForce(force * power, ForceMode2D.Impulse);
            lb.EndLine();
            
            jumpCount -= 1;
        }
    }
}
