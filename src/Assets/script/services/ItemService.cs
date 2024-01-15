using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.script.model;

namespace Assets.script.services
{
    public class ItemService
    {
        public SaveFile saveFile;
        public int chestId = -1;
        public JsonDataService DataService = new JsonDataService();
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
            catch
            {

            }
        }

        private void DeserializeStaticSave()
        {
            try
            {
                saveFile = DataService.LoadData<SaveFile>($"/staticSaveData.json", false);
            }
            catch
            {

            }
        }
        public void AddItem(List<Item> items)
        {
            DeserializeStaticSave();
            foreach (Item item in items)
            {
                if (item.consumable == false)
                {
                    saveFile.items.Add(item);
                }
                else
                {
                    int itemIndex = saveFile.items.FindIndex(searchItem => item.name == searchItem.name);
                    if (itemIndex == -1)
                    {
                        saveFile.items.Add(item);
                    }
                    else
                    {
                        saveFile.items[itemIndex].amount += 1;
                    }
                }
            }
            if(chestId > -1)
            {
                saveFile.mapData.chestList[chestId].opened = true;
            }
            SerializeStaticSave();
        }
        public void AddGold(int gold)
        {
            DeserializeStaticSave();
            saveFile.Gold += gold;
            SerializeStaticSave() ;
        }
    }
}
