using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataController : MonoBehaviour
{
    public static PlayerData playerData;

    private void Awake()
    {
        Load();
    }

    [ContextMenu("Load Data")]
    private void Load()
    {
        var data = PlayerPrefs.GetString("GameData");
        playerData = JsonUtility.FromJson<PlayerData>(data);
    }

    [ContextMenu("Save Data")]
    private void Save()
    {
        var data = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString("GameData", data);
    }

    private void OnDisable()
    {
        Save();
    }

    public static void SetData(GameObject o)
    {
        if (playerData.blocks == null)
            playerData.blocks = new List<GameObject>();

        playerData.blocks.Add(o);
    }
}

[System.Serializable]
public struct PlayerData
{
    // doesnt work, because gameobject is not serializable
    public List<GameObject> blocks;
}