using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrackHealth : MonoBehaviour
{
    public FloatData hpAmount;
    public UnityEvent OnAmount;

    private void Update()
    {
        if (hpAmount.value <= 0)
        {
            OnAmount.Invoke();
            StartCoroutine(FixHealth());
        }
    }

    private IEnumerator FixHealth()
    {
        yield return new WaitForSeconds(1);
        hpAmount.value = 1;
    }
}
