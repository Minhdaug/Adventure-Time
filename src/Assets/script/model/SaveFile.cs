using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.script.model
{
    public class SaveFile
    {
        public List<Character> characters = new List<Character>
        {
            new Character(){
                charClass = CharClass.warrior,
                maxHp = 300f,
                maxMp = 90f,
                currHp = 300f,
                currMp = 90f,
                atkPow = 20f,
                magPow = 15,
                defStat = 20f,
                equippedWeapon = "Man Gauntlet",
                equippedArmor = "Man Armor",
                skills = new List<string>{ "fire 1", "heal 1" },
            },
            new Character(){
                charClass = CharClass.mage,
                maxHp = 300f,
                maxMp = 90f,
                currHp = 300f,
                currMp = 90f,
                atkPow = 20f,
                magPow = 15,
                defStat = 20f,
                equippedWeapon = "Woman Gauntlet",
                equippedArmor = "Woman Armor",
                skills = new List<string>{ "fire 1", "heal 1" },
            },
            new Character(){
                charClass = CharClass.healer,
                maxHp = 300f,
                maxMp = 90f,
                currHp = 300f,
                currMp = 90f,
                atkPow = 20f,
                magPow = 15,
                defStat = 20f,
                equippedWeapon = "Woman Gauntlet",
                equippedArmor = "Woman Armor",
                skills = new List<string>{ "fire 1", "heal 1" },
            }
        };
        public Map mapData = new Map();
        public DateTime saveTime = DateTime.Now;
    }
}
