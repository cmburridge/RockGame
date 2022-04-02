using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    public Movement movement;
    public Rigidbody2D rb;
    public float reverseGravity;

    void Update()
    {
        while (movement.jumpCount == 0)
        {
            rb.gravityScale = reverseGravity;
        }
    }
}
