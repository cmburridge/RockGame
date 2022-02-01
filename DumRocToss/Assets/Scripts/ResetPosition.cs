using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public GameObject player;

    public void ChangePosition()
    {
        player.transform.Rotate(0,0,180);
    }
}
