using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.script.model;

namespace Assets.script.defaultData
{
    public class EnemyList
    {
        public readonly List<List<string>> EnemiesInCombat = new List<List<string>>()
        {
            new List<string>() {"Slime", "Slime"}, // 1
            new List<string>() {"Slime", "Slime"}, // 2
            new List<string>() {"Slime", "Slime", "Slimne"}, //3
            new List<string>() {"Slime", "Slime", "Slime", "Slime"}, //4
            new List<string>() {"Bat", "Slime"}, //5
            new List<string>() {"Bat", "Bat", "Slime"}, //6
            new List<string>() {"Bat", "Bat", "Bat"}, //7
            new List<string>() {"Bat", "Bat", "Bat", "Bar"}, //8
            new List<string>() {"Goblin", "Bat", "Slime"}, //9
            new List<string>() {"Goblin", "Bat", "Bat"}, //10
            new List<string>() {"Goblin", "Goblin", "Goblin"}, //11
            new List<string>() {"Goblin", "Goblin", "Goblin", "Goblin"}, //12
            new List<string>() {"Goblin", "Goblin", "Skeleton"}, //13
            new List<string>() {"Goblin", "Goblin", "Goblin", "Skeleton"}, //14
            new List<string>() {"GolemBoss"}, //15
            new List<string>() { "HeraldOfDeath" }, //16
            new List<string>() { "GrimReaper" }, //17
            new List<string>() { "ZombieWarrior", "ZombieWarrior" }, //18
            new List<string>() { "Skull", "Skull" }, //19

        };

        public List<EnemyModel> EnemiesOnMap = new List<EnemyModel>()
        {
            new EnemyModel() {id = 0, isAlive = true},
            new EnemyModel() {id = 1, isAlive = true},
            new EnemyModel() {id = 2, isAlive = true},
            new EnemyModel() {id = 3, isAlive = true},
            new EnemyModel() {id = 4, isAlive = true},
            new EnemyModel() {id = 5, isAlive = true},
            new EnemyModel() {id = 6, isAlive = true},
            new EnemyModel() {id = 7, isAlive = true},
            new EnemyModel() {id = 8, isAlive = true},
            new EnemyModel() {id = 9, isAlive = true},
            new EnemyModel() {id = 10, isAlive = true},
            new EnemyModel() {id = 11, isAlive = true},
            new EnemyModel() {id = 12, isAlive = true},
            new EnemyModel() {id = 13, isAlive = true},
            new EnemyModel() {id = 14, isAlive = true},
        };
    }
}
