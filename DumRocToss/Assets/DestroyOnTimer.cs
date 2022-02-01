using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTimer : MonoBehaviour
{
    public GameObject prefab;
    public GameObject spawner;
    public Vector3 location;

    private void Start()
    {
        location = spawner.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(prefab, location, Quaternion.identity );
        Destroy(this.gameObject);
    }
}
