using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTimer : MonoBehaviour
{
    public GameObject prefab;
    public GameObject spawner;
    public Vector3 location;
    public float time;

    private void Start()
    {
        location = spawner.transform.position;
        StartCoroutine(DestroyThis());
    }

    private IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(time);
        Instantiate(prefab, location, Quaternion.identity );
        Destroy(this.gameObject);
    }
}
