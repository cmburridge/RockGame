using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnMouseEvent : MonoBehaviour
{
    public bool switchOn = false;
    public Rigidbody2D rb;
    public float reverseGravity;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            switchOn = true;
        }
        else if (Input.touchCount <= 0)
        {
            switchOn = false;
        }
        
        if (switchOn == true)
        {
            rb.gravityScale = reverseGravity;
        }
        else
        {
            rb.gravityScale = 1;
        }
    }
}
