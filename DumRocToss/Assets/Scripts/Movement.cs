using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float power;
    public float speed;
    public float gravity = 1;
    
    public Rigidbody2D rb;
    public Camera cam;
    
    public Vector2 minPower;
    public Vector2 maxPower;
    
    private Vector2 force;
    public Vector2 reset;

    private bool isMoving;
    private bool isGrounded;
    public GameObject GroundCheck;
    
    private bool isJumping;
    public float jumpCount;
    public float jumpCountMax;
    public float speedCap = 6;
    public MapData MD;
    
    private Vector3 startPoint;
    private Vector3 endPoint;
    
    public GameObject indicator;
    public SpriteRenderer art;

    private void Start()
    {
        cam = Camera.main;

        if (MD.isOcean == true)
        {
            rb.gravityScale = gravity;
            power = 1;
            jumpCountMax = 4;
            art.color = Color.cyan;
        }
        else
        {
            gravity = 1;
        }
    }

    private void Update()
    {
        if (gravity == 0)
        {
            jumpCount = 1;
        }
        
        speed = rb.velocity.magnitude;
        if (speed < speedCap && jumpCount > 0)
        {
            isMoving = false;
            indicator.SetActive(true);
        }
        else
        {
            isMoving = true;
            indicator.SetActive(false);
        }
        
        if (GroundCheck.gameObject.activeInHierarchy && speed < speedCap)
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
        
        if (jumpCount > 0 && Input.GetMouseButtonDown(0))
        {
            startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 15; 
        }

        if (jumpCount > 0 && Input.GetMouseButtonUp(0) && isMoving == false)
        {
            rb.gravityScale = gravity;
            rb.freezeRotation = false;
            
            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 15;
            
            force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
            rb.AddForce(force * power, ForceMode2D.Impulse);
            
            jumpCount -= 1;
        }
    }

    private void OnMouseDown()
    {
        if (jumpCount == 0)
        {
            rb.AddForce(reset, ForceMode2D.Impulse);   
        }
    }
}
