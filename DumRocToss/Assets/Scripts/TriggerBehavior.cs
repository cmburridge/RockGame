using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBehavior : MonoBehaviour
{
    public UnityEvent triggered;
    public UnityEvent unTriggered;

    private bool active = true;
    
    public float time;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (active == false)
        {
            return;
        }
        else
        {
            triggered.Invoke();   
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        unTriggered.Invoke();
    }

    public void SetInActive()
    {
        StartCoroutine(GracePeriod());
    }
    

    private IEnumerator GracePeriod()
    {
        active = false;
        yield return new WaitForSeconds(time);
        active = true;
    }
}
