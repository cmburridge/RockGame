using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.attachedRigidbody.gravityScale = 0;
        other.attachedRigidbody.velocity = Vector2.zero;
        other.attachedRigidbody.freezeRotation = true;
    }
}
