using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffects : MonoBehaviour
{
    public void SpawnEffectHere(GameObject effect)
    { 
        Instantiate(effect,this.transform.position,this.transform.rotation);
    }
}
