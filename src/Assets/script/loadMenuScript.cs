using Assets.script.model;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class loadMenuScript : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> saveSlots;

    private SaveFile SaveFile = new SaveFile();
    private IDataService DataService = new JsonDataService();

    private void SaveToStaticData()
    {
        try
        {
            DataService.SaveData($"/staticSaveData.json", SaveFile, false);
        }
        catch (Exception e)
        {
            Debug.LogError($"Could not save file! error : {e}");
        }
    }

    private bool GetSaveFiles(int saveSlot)
    {
        try
        {
            SaveFile data = DataService.LoadData<SaveFile>($"/save-file{saveSlot}.json", false);
            SaveFile = data;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void chooseSave(int saveSlot)
    {
        if (saveSlot == 4) // 4 == newgame
        {
            SaveFile = new SaveFile();
            SaveToStaticData();
            SceneManager.LoadScene("scene-1");
        }
        else if (GetSaveFiles(saveSlot))
        {
            SaveToStaticData();
            SceneManager.LoadScene("scene-1");
        }

    }

    public void exitGame()
    {
        Application.Quit();
    }

    private void Awake()
    {
        if (saveSlots.Count > 0)
        {
            for (int i = 1; i <= saveSlots.Count; i++)
            {
                if (GetSaveFiles(i)) saveSlots[i].SetText($"{SaveFile.saveTime}");
                else saveSlots[i - 1].SetText($"Save File {i}");
            }
        }
    }
}
