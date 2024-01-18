using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.script.model;
using Assets.script.defaultData;

namespace Assets.script.services
{
    public class CombatService
    {
        private SaveFile saveFile;
        private CombatData combatData = new CombatData();
        private List<List<string>> enemies = new EnemyList().EnemiesInCombat;
        private JsonDataService dataService = new JsonDataService();

        private void DeserializeStaticSave()
        {
            try
            {
                saveFile = dataService.LoadData<SaveFile>($"/staticSaveData.json", false);
            }
            catch
            { }
        }


        public void SaveCombateData(int enemyId)
        {
            DeserializeStaticSave();
            combatData.characterLvl["Mercy"] = saveFile.characters[0].level;
            combatData.characterLvl["Inirius"] = saveFile.characters[1].level;
            combatData.characterLvl["Eni"] = saveFile.characters[2].level;
            combatData.enemiesInAct = enemies[enemyId];
            try
            {
                dataService.SaveData<CombatData>("/combatData.json", combatData, false);
            }
            catch
            {
                throw new Exception();
            }
        }

        public CombatData LoadCombatData()
        {
            try
            {
                combatData = dataService.LoadData<CombatData>("/combatData.json", false);
                return combatData;
            }
            catch (Exception e)
            {
                throw new Exception();
            }
        }
    }
}
