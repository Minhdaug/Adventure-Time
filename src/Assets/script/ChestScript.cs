using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.script.services;
using Assets.script.defaultData;
using Assets.script.model;

public class ChestScript : MonoBehaviour
{
    public int ChestId;
    private ItemService ItemMethod = new ItemService();
    private Chest ChestInfo;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChestInfo = new ChestData() { }.ChestDefaultList[ChestId];
        ItemMethod.chestId = ChestId;
        if (collision.gameObject.tag == "Player")
        {
            if (ChestInfo.itemReward != null)
            {
                ItemMethod.AddItem(new ChestData().ChestDefaultList[ChestId].itemReward);
            }
            else
            {
                ItemMethod.AddGold(ChestInfo.goldReward);
            }
        }
        gameObject.SetActive(false);
    }
}
