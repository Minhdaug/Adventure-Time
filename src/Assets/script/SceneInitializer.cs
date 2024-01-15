using Assets.script.model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class SceneInitializer : MonoBehaviour
{
    public List<GameObject> EnemyObjects;
    public List<GameObject> ChestObjects;
    public GameObject player;

    private SaveFile saveFile = new();
    private JsonDataService DataService = new();

    public GameObject menuCanvas;

    private void SerializeStaticSave()
    {
        try
        {
            DateTime saveTime = DateTime.Now;
            saveFile.saveTime = saveTime;
            {
                DataService.SaveData($"/staticSaveData.json", saveFile, false);
            }

        }
        catch (Exception e)
        {
            Debug.LogError($"Could not save file! error : {e}");
        }
    }

    private void DeserializeStaticSave()
    {
        try
        {
            saveFile = DataService.LoadData<SaveFile>($"/staticSaveData.json", false);
        }
        catch (Exception e)
        {
            Debug.LogError($"Could not read file! error: {e}");
        }
    }

    private void LoadEnemyStatus()
    {
        for (int enemyIndex = 0;  enemyIndex < EnemyObjects.Count;  enemyIndex++)
        {
            EnemyObjects[enemyIndex].SetActive(saveFile.mapData.enemyList[enemyIndex].isAlive);
        }
    }

    private void LoadChestStatus()
    {
        for (int chestIndex = 0; chestIndex < ChestObjects.Count; chestIndex++)
        {
            ChestObjects[chestIndex].SetActive(!saveFile.mapData.chestList[chestIndex].opened);
        }
    }

    private void setPlayerPostion()
    {
        player.transform.position = new Vector3(saveFile.mapData.currPosX, saveFile.mapData.currPosY, 0);
    }

    void Awake()
    {
        DeserializeStaticSave();
        LoadChestStatus();
        LoadEnemyStatus();
        setPlayerPostion();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }
    }
}
