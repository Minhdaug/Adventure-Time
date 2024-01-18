using Assets.script.model;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.script.defaultData;
using Assets.script.services;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> ItemNameDescription; // 0 == name, 1 == description, 2 == message, 3 == remain gold
    public GameObject ItemList;

    private List<Item> allItems = new ItemListData().ShopItems;
    private ItemService itemService = new ItemService();
    private Item selectedItem;
    private int selectedItemIndex = 0;
    private int maxItemIndex = 9;
    private int minItemIndex = 0;
    // Start is called before the first frame update

    private Button getChildButton(GameObject parentObject, int index)
    {
        return parentObject.transform.GetChild(index).gameObject.GetComponent<Button>();
    }
    private TextMeshProUGUI getButtonText(Button parentObject)
    {
        return parentObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void LoadItem()
    {
        ItemNameDescription[3].SetText($"Gold : {itemService.getGold()}");
        for (int i = 0; i < 10; i++)
        {
            if (minItemIndex + i >= allItems.Count)
            {
                getButtonText(getChildButton(ItemList, i)).text = $"";
            }
            else
            {
                getButtonText(getChildButton(ItemList, i)).text = $"{allItems[minItemIndex + i].name}";
            }
        }
        UpdateSelectedItemInfo();
    }
    public void GoToNextItemPage(GameObject ItemList)
    {
        if (maxItemIndex >= allItems.Count)
        {
            minItemIndex = 0;
            maxItemIndex = 10;
            LoadItem();
        }
        else
        {
            minItemIndex += 10;
            maxItemIndex += 10;
            LoadItem();

        }
    }

    private void UpdateSelectedItemInfo()
    {
        selectedItem = allItems[selectedItemIndex];
        ItemNameDescription[0].SetText(selectedItem.name);
        ItemNameDescription[1].text = $"{selectedItem.description}. Cost : {selectedItem.cost} Gold";
    }

    public void ChangeSelectedItem(int itemIndex) // itemIndex range : 0 -> 9
    {
        selectedItemIndex = itemIndex + minItemIndex;
        if (selectedItemIndex < allItems.Count)
        {
            selectedItem = allItems[selectedItemIndex];
            UpdateSelectedItemInfo();
        }
    }

    public void BuyItem()
    {
        if (itemService.AddGold(-selectedItem.cost))
        {
            itemService.AddItem(new List<Item> { selectedItem });
            ItemNameDescription[2].SetText("Purchased !");
        } else
        {
            ItemNameDescription[2].SetText("Not enough gold !!!");
        }
        ClearMessage();
    }

    async Task ClearMessage()
    {
        await Task.Delay(1000);
        ItemNameDescription[2].text = string.Empty;
    }

    private void Awake()
    {
        LoadItem();
    }
}
