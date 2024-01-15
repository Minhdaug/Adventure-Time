using Assets.script.model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.script.defaultData;

public class Shop : MonoBehaviour
{
    public List<TextMeshProUGUI> ItemNameDescription; // 0 == name, 1 == description
    public GameObject ItemList;

    private List<Item> allItems = new ItemListData().ShopItems;
    private Item selectedItem;
    private int selectedItemIndex = 0;
    private int maxItemIndex = 10;
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
        if (maxItemIndex > allItems.Count)
        {
            minItemIndex = 0;
            maxItemIndex = 10;
        }
        UpdateSelectedItemInfo();
    }
    public void GoToNextItemPage(GameObject ItemList)
    {
        if (maxItemIndex > allItems.Count)
        {
            minItemIndex = 0;
            maxItemIndex = 10;
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
        ItemNameDescription[0].SetText(selectedItem.name);
        ItemNameDescription[1].SetText(selectedItem.description);
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

    private void Awake()
    {
        LoadItem();
    }
}
