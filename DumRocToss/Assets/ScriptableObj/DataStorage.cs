using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataStorage", menuName = "Utilities/Data Storage Object")]
public class DataStorage : ScriptableObject
{
    public ScriptableObject data;
    public List<ScriptableObject> listData;

    public void SetListData()
    {
        foreach (var obj in listData)
        {
            SetData(obj);
        }

        SaveNow();
    }

    public void GetListData()
    {
        foreach (var obj in listData)
        {
            GetData(obj);
        }
    }

    public void SetData(ScriptableObject obj)
    {
        if (obj == null) return;

        string json = JsonUtility.ToJson(obj);
        PlayerPrefs.SetString(obj.name, json);
    }

    public void SetData()
    {
        if (data == null) return;

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(data.name, json);

        SaveNow();
    }

    public void GetData(ScriptableObject obj)
    {
        if (obj == null) return;

        if (PlayerPrefs.HasKey(obj.name))
        {
            string json = PlayerPrefs.GetString(obj.name);
            JsonUtility.FromJsonOverwrite(json, obj);
        }
    }

    public void GetData()
    {
        if (data == null) return;

        if (PlayerPrefs.HasKey(data.name))
        {
            string json = PlayerPrefs.GetString(data.name);
            JsonUtility.FromJsonOverwrite(json, data);
        }
    }

    private void SaveNow()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        PlayerPrefs.Save();
#endif
    }
}
