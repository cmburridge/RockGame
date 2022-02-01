using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public GameObject check;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        check.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        check.SetActive(false);
    }
}
