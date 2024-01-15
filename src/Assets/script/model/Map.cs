using System.Collections.Generic;
using Assets.script.defaultData;

namespace Assets.script.model
{
    public class Map
    {
        public float currPosX = 12;
        public float currPosY = 0; 
        public List<Chest> chestList = new ChestData().ChestDefaultList;
        public List<EnemyModel> enemyList = new List<EnemyModel>(){
            new EnemyModel() {
                id = 0,
                name = EnemyName.Slime,
                isAlive = true,
            }, new EnemyModel() {
                id = 1,
                name = EnemyName.Orc,
                isAlive = true,
            }
        };
    }
}
