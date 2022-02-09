using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomScale : MonoBehaviour
{
    public float minScale;
    public float maxScale;

    private void Start()
    {
        transform.localScale *= Random.Range(minScale, maxScale);
    }
}
