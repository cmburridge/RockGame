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
    public bool isArmored = false;
    public bool isFragile = false;
    public GameObject prefab;

    private void Start()
    {
        if (isArmored == true)
        {
            hpAmount.value = 4;
        }
        else
        {
            hpAmount.value = 3;
        }

        if (isFragile == true)
        {
            hpAmount.value = 1;
        }
        else
        {
            hpAmount.value = 3;
        }
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
            Instantiate(prefab, this.transform.position, Quaternion.identity);
            healthEvent.Invoke();
        }
    }
}
