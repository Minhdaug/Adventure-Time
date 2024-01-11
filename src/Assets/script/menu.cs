using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.script.model;

public class menu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI SourceDataText;
    [SerializeField]
    private TMP_InputField InputField;
    [SerializeField]
    private TextMeshProUGUI SaveTimeText;
    [SerializeField]
    private TextMeshProUGUI LoadTimeText;

    private SaveFile SaveFile = new SaveFile();
    private IDataService DataService = new JsonDataService();
    private bool EncryptionEnabled = false;
    public void SerializeJson(int saveSlot)
    {
        try {
            DateTime saveTime = DateTime.Now;
            SaveFile.saveTime = saveTime;
            DataService.SaveData($"/save-file{saveSlot}.json", SaveFile, EncryptionEnabled);
        }
        catch (Exception e) 
        {
            Debug.LogError($"Could not save file! error : {e}");
        }
    }

    public void DeserializeJson(int saveSlot)
    {
        try
        {
            SaveFile data = DataService.LoadData<SaveFile>($"/save-file{saveSlot}.json", EncryptionEnabled);
            SaveFile = data;
        }
        catch (Exception e)
        {   
            Debug.LogError($"Could not read file! error: {e}");
        }

    }



    private void Awake()
    {
        SourceDataText.SetText(JsonConvert.SerializeObject(SaveFile, Formatting.Indented));
    }
}
