using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class MapData : ScriptableObject
{
    public bool isOcean = false;
    public bool isMute;

    public void MuteAudio()
    {
        isMute = true;
    }

    public void UnMute()
    {
        isMute = false;
    }
}
