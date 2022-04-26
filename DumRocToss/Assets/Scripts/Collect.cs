using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collect : MonoBehaviour
{
    public UnityEvent onCollect;
    public AudioSource audioS;
    public float timer;
    public IntData cash;

    private void OnTriggerEnter2D(Collider2D other)
    {
        audioS.Play();
        cash.value += 1;
        StartCoroutine(AfterEffect());
    }

    private IEnumerator AfterEffect()
    {
        yield return new WaitForSeconds(timer);
        onCollect.Invoke();
    }
}
