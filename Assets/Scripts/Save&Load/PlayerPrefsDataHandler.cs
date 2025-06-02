using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsDataHandler
{
    private string dataKey = "GameData";

    public PlayerPrefsDataHandler(bool _encryptData)
    {
        // Encryption is no longer used, so this constructor parameter is ignored.
    }

    public void Save(GameData _data)
    {
        string dataToStore = JsonUtility.ToJson(_data, true);
        PlayerPrefs.SetString(dataKey, dataToStore);
        PlayerPrefs.Save();
    }

    public GameData Load()
    {
        if (PlayerPrefs.HasKey(dataKey))
        {
            string dataToLoad = PlayerPrefs.GetString(dataKey);
            return JsonUtility.FromJson<GameData>(dataToLoad);
        }
        return null;
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteKey(dataKey);
    }
}
