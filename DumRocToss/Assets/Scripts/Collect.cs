using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collect : MonoBehaviour
{
    public UnityEvent onCollect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        onCollect.Invoke();
    }
}
