using Assets.script.model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject shop;

    private SaveFile saveFile = new SaveFile();
    private JsonDataService dataService = new JsonDataService();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        saveFile = dataService.LoadData<SaveFile>($"/staticSaveData.json", false);
        if (collision.gameObject.tag == "Player")
        {
            shop.SetActive(true);
            foreach (Character character in saveFile.characters)
            {
                character.currHp = character.maxHp;
                character.currMp = character.maxMp;
            }
            foreach (EnemyModel enemy in saveFile.mapData.enemyList)
            {
                enemy.isAlive = true;
            }
            dataService.SaveData($"/staticSaveData.json", saveFile, false);
        }
    }
}
