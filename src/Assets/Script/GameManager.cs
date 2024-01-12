using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _playerPrefab;
    [SerializeField] Button _saveBtn;
    PositionData position;

    public class PositionData
    {
        public float x, y;
    };

    void Start()
    {
        _saveBtn.onClick.AddListener(SaveData);
        position = new PositionData();
    }
    void Update()
    {
        position.x = _playerPrefab.transform.position.x;
        position.y = _playerPrefab.transform.position.y;


        //Debug.Log($"{_playerPrefab.transform.position}");
    }

    //public static void SavePlayer (Player player)
    //{
    //    BinaryFormatter formatter = new BinaryFormatter();

    //    string path = Application.persistentDataPath + "/player.fun";
    //    FileStream stream = new FileStream(path, FileMode.Create);

    //    //PlayerData data = new PlayerData(player);

    //    formatter.Serialize(stream, data);
    //    stream.Close();
    //}

    //public static PlayerData LoadPlayer()
    //{
    //    string path = Application.persistentDataPath + "/player.fun";
    //    if(File.Exists(path))
    //    {
    //        BinaryFormatter formatter = new BinaryFormatter();
    //        FileStream stream = new FileStream(path, FileMode.Open);

    //        PlayerData data = formatter.Deserialize(stream) as PlayerData;
    //        stream.Close();

    //        return data;
    //    } else
    //    {
    //        Debug.LogError("Save file not found in " + path);
    //        return null;
    //    }
    //}

    public void SaveData ()
    {
        string json = JsonUtility.ToJson( position );

        string path = Application.dataPath + "/Save/data.json";

        File.WriteAllText( path, json );

        Debug.Log("Data written");
    }
}
