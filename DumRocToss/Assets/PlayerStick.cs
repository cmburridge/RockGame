using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PlayerStick : MonoBehaviour
{
    public Rigidbody2D rb;

    private float random = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        random = Random.Range(1,4);
        RollChance();
    }

    private void RollChance()
    {
        if (random == 2)
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero; 
            rb.freezeRotation = true;
        }
        else
        {
            return;
        }
    }
}
