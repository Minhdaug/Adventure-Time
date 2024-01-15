using System.Collections.Generic;
using Assets.script.defaultData;

namespace Assets.script.model
{
    public class Map
    {
        public float currPosX = 12;
        public float currPosY = 0; 
        public List<Chest> chestList = new ChestData().ChestDefaultList;
        public List<Enemy> enemyList = new List<Enemy>(){
            new Enemy() {
                id = 0,
                name = EnemyName.Slime,
                isAlive = true,
            }, new Enemy() {
                id = 1,
                name = EnemyName.Orc,
                isAlive = true,
            }
        };
    }
}
