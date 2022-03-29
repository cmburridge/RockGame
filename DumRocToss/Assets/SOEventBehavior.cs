using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SOEventBehavior : MonoBehaviour
{
    public UnityEvent changeSO;
    public MapData MD;
    public FloatData HP;
    
    private void Start()
    {
        changeSO.Invoke();
    }

    public void isOcean()
    {
        MD.isOcean = true;
    }

    public void isNotOcean()
    {
        MD.isOcean = false;
    }

    public void hpFull()
    {
        HP.value = 3;
    }
}
