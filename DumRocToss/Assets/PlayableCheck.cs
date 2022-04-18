using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayableCheck : MonoBehaviour
{
    public Purchasable item;
    public UnityEvent ifBought;
    void Update()
    {
        if (item.Purchased == false)
        {
            return;
        }
        else
        {
            ifBought.Invoke();
        }
    }
}
