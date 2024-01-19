using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.script.services;
using Assets.script.defaultData;
using Assets.script.model;
using TMPro;

public class ChestScript : MonoBehaviour
{
    [SerializeField]
    private int ChestId;
    [SerializeField]
    private TextMeshProUGUI notification;

    private ItemService ItemMethod = new ItemService();
    private Chest ChestInfo;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChestInfo = new ChestData() { }.ChestDefaultList[ChestId];
        ItemMethod.chestId = ChestId;
        string notificationText = "";
        if (collision.gameObject.tag == "Player")
        {
            if (ChestInfo.itemReward != null)
            {
                ItemMethod.AddItem(new ChestData().ChestDefaultList[ChestId].itemReward);
                notificationText = "Get " + ChestInfo.itemReward[0].name + " X " + ChestInfo.itemReward.Count;
                setNotication(notification, notificationText);
            }
            else
            {
                ItemMethod.AddGold(ChestInfo.goldReward);
                notificationText = "Get " + ChestInfo.goldReward + " gold";
                setNotication(notification, notificationText);
            }
        }
        gameObject.SetActive(false);
    }
	private async void setNotication(TextMeshProUGUI notify, string information)
    {
        Debug.Log($"information: {information}");
        Debug.Log($"notification: {notification}");
		notify.SetText(information);
        await Task.Delay(1000);
		notify.SetText("");
    }
}
