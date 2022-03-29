using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBehavior : MonoBehaviour
{
    public UnityEvent healthEvent;
    public FloatData hpAmount;
    public GameObject damage1, damage2;

    private void Start()
    {
        hpAmount.value = 3;
    }

    void Update()
    {
        if (hpAmount.value == 2)
        {
            damage1.SetActive(true);
        }
        
        if (hpAmount.value == 1)
        {
            damage2.SetActive(true);
        }

        if (hpAmount.value <= 0)
        {
            healthEvent.Invoke();
        }
    }
}
