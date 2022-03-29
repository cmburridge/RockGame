using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public FloatData hp;

    public void LowerHp()
    {
        hp.value -= 1;
    }
}
