using Assets.script.model;
using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// if saveSlot > 3 then save file = staticSaveSlot

public class menu : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Contents;
    [SerializeField]
    public List<TextMeshProUGUI> SaveLoadButtonText;

    private SaveFile TempData = new SaveFile();
    private SaveFile SaveFile = new SaveFile();
    private IDataService DataService = new JsonDataService();

    //character stuffs
    public Sprite heroSprite;
    public Sprite mageSprite;
    public Sprite healerSprite;
    private Character character;
    private int currCharIndex = 0;

    // item
    public List<TextMeshProUGUI> ItemNameDescription; // 0 == name, 1 == description
    private Item selectedItem;
    private int selectedItemIndex = 0;
    private int maxItemIndex = 10;
    private int minItemIndex = 0;

    // skill
    public List<TextMeshProUGUI> SkillNameDescription; // 0 == name, 1 == description
    private SkillModel selectedSkill;

    public GameObject player;

    //read and write save file
    public void SerializeJson(int saveSlot)
    {
        try
        {
            DateTime saveTime = DateTime.Now;
            SaveFile.saveTime = saveTime;
            SaveFile.mapData.currPosY = player.transform.position.y;
            SaveFile.mapData.currPosX = player.transform.position.x;
            if (saveSlot > 3)
            {
                DataService.SaveData($"/staticSaveData.json", SaveFile, false);
            }
            else
            {
                DataService.SaveData($"/save-file{saveSlot}.json", SaveFile, false);
            }

        }
        catch (Exception e)
        {
            Debug.LogError($"Could not save file! error : {e}");
        }
    }

    public void DeserializeJson(int saveSlot, bool isLoadAgain)
    {
        try
        {
            if (saveSlot > 3)
            {
                SaveFile = DataService.LoadData<SaveFile>($"/staticSaveData.json", false);
                character = SaveFile.characters[currCharIndex];
                selectedItem = SaveFile.items[0];
                selectedSkill = character.skills[0];
            }
            else
            {
                TempData = DataService.LoadData<SaveFile>($"/save-file{saveSlot}.json", false);
                if (isLoadAgain)
                {
                    SaveFile = TempData;
                    character = SaveFile.characters[currCharIndex];
                    selectedItem = SaveFile.items[0];
                    selectedSkill = character.skills[0];
                }
            }
        }
        catch (Exception e)
        {
            TempData = null;
            Debug.LogError($"Could not read file! error: {e}");
        }

    }

    //reused methods
    public void GoToNextCharacter()
    {
        if (currCharIndex >= 2)
        {
            currCharIndex = 0;
        }
        else
        {
            currCharIndex++;
        }
        character = SaveFile.characters[currCharIndex];
    }
    public void UpdateCharacterSprite(GameObject CharacterSprite)
    {
        if (character.charClass == CharClass.warrior)
        {
            CharacterSprite.GetComponent<SpriteRenderer>().sprite = heroSprite;
        }
        else if (character.charClass == CharClass.mage)
        {
            CharacterSprite.GetComponent<SpriteRenderer>().sprite = mageSprite;
        }
        else
        {
            CharacterSprite.GetComponent<SpriteRenderer>().sprite = healerSprite;
        }
    }

    public void OpenContents(GameObject displayedContent)
    {
        foreach (GameObject content in Contents)
        {
            content.SetActive(false);
        }
        displayedContent.SetActive(true);
    }

    private Button getChildButton(GameObject parentObject, int index)
    {
        return parentObject.transform.GetChild(index).gameObject.GetComponent<Button>();
    }
    private TextMeshProUGUI getButtonText(Button parentObject)
    {
        return parentObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }



    //character status methods
    public void UpdateCharacterInfo(TextMeshProUGUI CharacterStatus)
    {
        CharacterStatus.text = $"Level - {character.level}<br>" +
            $"Next Level - {character.currExp}/{character.neededExp}<br>" +
            $"HP - {character.currHp}/{character.maxHp}<br>" +
            $"MP - {character.currMp}/{character.maxMp}<br>" +
            $"Attack - {character.atkPow}<br>" +
            $"Defend - {character.defStat}<br>" +
            $"Magic - {character.magic}<br>" +
            $"Strength - {character.strength}<br>" +
            $"Endurance - {character.endurance}<br>" +
            $"Speed - {character.speed}<br>" +
            $"Equipped Amor - {character.equippedArmor}<br>" +
            $"Equipped Weapon - {character.equippedWeapon}";
    }
    public void UpdateCharcterName(TextMeshProUGUI CharacterName)
    {
        if (character.charClass == CharClass.warrior)
        {
            CharacterName.SetText("Mercy");
        }
        else if (character.charClass == CharClass.mage)
        {
            CharacterName.SetText("Inirius");
        }
        else
        {
            CharacterName.SetText("Eni");
        }
    }

    //save game methods
    public void UpdateSaveTime(TextMeshProUGUI SaveDate)
    {
        SaveDate.SetText(DateTime.Now.ToString());
    }
    private void UpdateTempData()
    {
        int buttonCounter = 0; // 0-1 = save/load of saveslot 1, 2-3 = ...
        for (int i = 1; i < 4; i++)
        {

            DeserializeJson(i, false);
            if (TempData != null)
            {
                SaveLoadButtonText[buttonCounter].SetText(TempData.saveTime.ToString());
                buttonCounter++;
                SaveLoadButtonText[buttonCounter].SetText(TempData.saveTime.ToString());
                buttonCounter++;

            }

        }
    }

    //load game methods
    public void LoadGame(int Slot)
    {
        DeserializeJson(Slot, true);
        SerializeJson(4);
        SceneManager.LoadScene("scene-1");
    }

    //item content methods
    public void LoadItem(GameObject ItemList)
    {
        for (int i = 0; i < 10; i++)
        {
            if (minItemIndex + i >= SaveFile.items.Count)
            {
                getButtonText(getChildButton(ItemList, i)).text = $"";
            }
            else
            {
                getButtonText(getChildButton(ItemList, i)).text = $"{SaveFile.items[minItemIndex + i].name} X {SaveFile.items[minItemIndex + i].amount}";
            }
        }
        if (maxItemIndex > SaveFile.items.Count)
        {
            minItemIndex = 0;
            maxItemIndex = 10;
        }
        UpdateSelectedItemInfo();
    }
    public void GoToNextItemPage(GameObject ItemList)
    {
        if (maxItemIndex > SaveFile.items.Count)
        {
            minItemIndex = 0;
            maxItemIndex = 10;
        }
        else
        {
            minItemIndex += 10;
            maxItemIndex += 10;
            LoadItem(ItemList);

        }
    }

    public void ChangeSelectedItem(int itemIndex) // itemIndex range : 0 -> 9
    {
        selectedItemIndex = itemIndex + minItemIndex;
        if (selectedItemIndex < SaveFile.items.Count)
        {
            selectedItem = SaveFile.items[selectedItemIndex];
            UpdateSelectedItemInfo();
        }
    }

    private void UpdateSelectedItemInfo()
    {
        ItemNameDescription[0].SetText(selectedItem.name);
        ItemNameDescription[1].SetText(selectedItem.description);
    }
    public void UpdateCharBasicInfo(TextMeshProUGUI info)
    {
        string text = $"";
        if (character.charClass == CharClass.warrior)
        {
            text += $"Mercy<br>";
        }
        else if (character.charClass == CharClass.mage)
        {
            text += $"Inirius<br>";
        }
        else
        {
            text += $"Eni<br>";
        }
        text += $"HP - {character.currHp}/{character.maxHp}<br>" +
            $"MP - {character.currMp}/{character.maxMp}<br>" +
            $"{character.equippedArmor}<br>" +
            $"{character.equippedWeapon}";
        info.text = text;
    }

    public void UseItemOnCharacter(GameObject message)
    {
        if (selectedItem.consumable)
        {
            if (character.maxHp > character.currHp && selectedItem.HPGain != 0 && selectedItem.amount > 0)
            {
                character.currHp += character.maxHp * selectedItem.HPGain;
                if (character.maxHp < character.currHp)
                {
                    character.currHp = character.maxHp;
                    UpdateCharInSave();
                }
                selectedItem.amount--;
                message.SetActive(false);
            }
            else if (character.maxMp > character.currMp && selectedItem.MPGain != 0 && selectedItem.amount > 0)
            {
                character.currMp += character.maxMp*selectedItem.MPGain;
                if (character.maxMp < character.currMp)
                {
                    character.currMp = character.maxMp;
                    UpdateCharInSave();
                }
                selectedItem.amount--;
                message.SetActive(false);
            }
            else
            {
                message.SetActive(true);
            }
            // if used all of a consumable item
            if (selectedItem.amount == 0)
            {
                SaveFile.items.RemoveAt(selectedItemIndex);
                selectedItemIndex = 0;
                selectedItem = SaveFile.items[selectedItemIndex];
            }
        }
        // use an equipment
        else
        {
            if (selectedItem.couldBeUseOn.Contains(character.charClass))
            {
                if (selectedItem.atkPowGain != 0 && selectedItem.equippedOn == CharClass.noClass)
                {
                    int equippedItemIndex = SaveFile.items.FindIndex(equippedItem => equippedItem.equippedOn == character.charClass && equippedItem.atkPowGain != 0);
                    UpdateEquipItem(equippedItemIndex, true);
                    message.SetActive(false);
                }
                else if (selectedItem.defGain != 0 && selectedItem.equippedOn == CharClass.noClass)
                {
                    int equippedItemIndex = SaveFile.items.FindIndex(equippedItem => equippedItem.equippedOn == character.charClass && equippedItem.defGain != 0);
                    UpdateEquipItem(equippedItemIndex, false);
                    message.SetActive(false);
                }
                else
                {
                    message.SetActive(true);
                }
            }
            else
            {
                message.SetActive(true);
            }
        }
    }

    private void UpdateEquipItem(int equippedItemIndex, bool isWeapon)
    {
        SaveFile.items[equippedItemIndex].equippedOn = CharClass.noClass;
        selectedItem.equippedOn = character.charClass;
        if (isWeapon)
        {
            character.equippedWeapon = selectedItem.name;
            character.atkPow = selectedItem.atkPowGain;
        }
        else
        {
            character.equippedArmor = selectedItem.name;
            character.defStat = selectedItem.defGain;
        }
        SaveFile.characters[currCharIndex] = character;
        SaveFile.items[selectedItemIndex] = selectedItem;
        SerializeJson(4);
    }

    private void UpdateCharInSave()
    {
        SaveFile.characters[currCharIndex] = character;
        SerializeJson(4);
    }

    //----------skill methods-------------//
    public void DisplaySkill(GameObject skillList)
    {
        int orderCounter = 0;
        bool showUnobtainedSkill = false;
        for (int i = 0; i < 10; i++)
        {
            if (!showUnobtainedSkill)
            {
                getButtonText(getChildButton(skillList, i)).text = $"{character.skills[orderCounter].name}";
                orderCounter++;
                if (orderCounter == character.skills.Count)
                {
                    orderCounter = 0;
                    showUnobtainedSkill = true;
                }
            }
            else
            {
                getButtonText(getChildButton(skillList, i)).text = $"{character.unobtainedSkills[orderCounter].name}";
                orderCounter++;
            }
        }
        UpdateSelectedSkillInfo();
    }
    public void ChangeSelectedSkill(int SkillPosition)
    {
        if (SkillPosition < character.skills.Count + character.unobtainedSkills.Count)
        {
            if (SkillPosition < character.skills.Count)
            {
                selectedSkill = character.skills[SkillPosition];
            }
            else
            {
                int unobtainedSkillPos = SkillPosition - character.skills.Count;
                selectedSkill = character.unobtainedSkills[unobtainedSkillPos];
            }

            UpdateSelectedSkillInfo();
        }
    }
    private void UpdateSelectedSkillInfo()
    {
        SkillNameDescription[0].text = selectedSkill.name;
        SkillNameDescription[1].text = selectedSkill.description;
    }

     void OnEnable()
    {
        DeserializeJson(4, false);
        UpdateTempData();
    }
}
