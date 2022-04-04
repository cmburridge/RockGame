using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject prefab;
    public PlayerData player;
    
    private void Start()
    {
        prefab = player.costume;
        GameObject obj = Instantiate(prefab, this.transform.position, Quaternion.identity);
        this.transform.SetParent(obj.transform);
    }
}
