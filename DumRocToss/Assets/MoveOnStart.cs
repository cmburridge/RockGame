using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnStart : MonoBehaviour
{
    public Vector2 move;
    public Rigidbody2D rb;
    
    void OnEnable()
    {
        rb.AddForce(move, ForceMode2D.Impulse);
    }
}
