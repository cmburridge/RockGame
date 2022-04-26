using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayableCheck : MonoBehaviour
{
    public Purchasable item;
    public UnityEvent ifBought;

    private void Start()
    {
        if (item.Purchased == true)
        {
            ifBought.Invoke();
        }
    }

    void Update()
    {
        if (item.Purchased == true)
        {
            ifBought.Invoke();
        }
    }
}
