using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Purchasable : ScriptableObject
{
    public int value;
    [SerializeField]private bool unLocked;
    [SerializeField]private bool purchased;

    public bool Unlocked
    {

        get => unLocked;
        set => unLocked = true;
    }
    public bool Purchased
    {
        get => purchased;
        set => purchased = value;
    }
}
