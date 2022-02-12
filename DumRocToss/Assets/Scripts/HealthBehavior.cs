using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBehavior : MonoBehaviour
{
    public UnityEvent healthEvent;
    public Slider healthBar;
    public FloatData hpAmount;
    public GameObject loseScreen;

    public void LowerHp()
    {
        hpAmount.value -= 1;
    }

    private void Start()
    {
        hpAmount.value = 3;
    }

    void Update()
    {
        healthBar.value = hpAmount.value;
        
        if (hpAmount.value <= 0)
        {
            loseScreen.SetActive(true);
            healthEvent.Invoke();
        }
    }
}
