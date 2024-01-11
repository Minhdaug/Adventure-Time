using System.Collections.Generic;

namespace Assets.script.model
{
    public class Map
    {
        public float currPosX = 0;
        public float currPosY = 0; 
        public List<Chest> chestList = new List<Chest>(){
            new Chest() {
                id = 0,
                goldReward = 100,
                itemReward = "",
                opened = false
            }, new Chest() {
                id = 0,
                goldReward = 100,
                itemReward = "",
                opened = false
            } 
        };
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
