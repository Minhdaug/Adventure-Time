using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.script.defaultData;

namespace Assets.script.model
{
    public class SaveFile
    {
        public List<Character> characters = new List<Character>
        {
            new Character(){
                charClass = CharClass.warrior,
                level = 5,
                neededExp = 2,
                currExp = 0,
                maxHp = 120f,
                maxMp = 40f,
                currHp = 120f,
                currMp = 40f,
                atkPow = 0f,
                magic = 4f,
                strength = 7f,
                endurance = 7f,
                speed = 3f,
                defStat = 0f,
                equippedWeapon = "Warrior sword 1",
                equippedArmor = "Warrior Armor 1",
                skills = new List<SkillModel>
                {
                    new SkillModel()
                    {
                        name = "Super Slash 1",
                        power = 65,
                        type = "physic",
                        description = "Deal light physic damage to an enemy. Require : Level 5",
                        hpGain = 0,
                        mpGain = -4f,
                        isAtkBuff = false,
                        isDefBuff = false,
                        isMultiTarget = false,
                        isTargetEnemy = true,
                        remainTurn = 0,
                    }
                },
                unobtainedSkills = new CharacterDefaultSkill().warriorUnobtained,
            },
            new Character(){
                charClass = CharClass.mage,
                level = 5,
                neededExp = 2,
                currExp = 0,
                maxHp = 300f,
                maxMp = 90f,
                currHp = 300f,
                currMp = 90f,
                atkPow = 20f,
                magic = 7f,
                strength = 4f,
                endurance = 7f,
                speed = 4f,
                defStat = 20f,
                equippedWeapon = "Spellcaster scepter 1",
                equippedArmor = "Spellcaster Armor 1",
                skills = new List<SkillModel>
                {
                    new SkillModel()
                    {
                        name = "Fire 1",
                        power = 40,
                        type = "fire",
                        description = "Deal light fire damage to an enemy. Require : Level 5",
                        hpGain = 0,
                        mpGain = -4f,
                        isAtkBuff = false,
                        isDefBuff = false,
                        isMultiTarget = false,
                        isTargetEnemy = true,
                        remainTurn = 0,
                    }
                },
                unobtainedSkills = new CharacterDefaultSkill().mageUnobtained
            },
            new Character(){
                charClass = CharClass.healer,
                level = 5,
                neededExp = 2,
                currExp = 0,
                maxHp = 300f,
                maxMp = 90f,
                currHp = 300f,
                currMp = 90f,
                atkPow = 20f,
                magic = 5,
                strength = 4f,
                endurance = 4f,
                speed = 5f,
                defStat = 20f,
                equippedWeapon = "Spellcaster scepter 1",
                equippedArmor = "Spellcaster Armor 1",
                skills = new List<SkillModel>
                {
                    new SkillModel()
                    {
                        name = "Heal 1",
                        power = 0,
                        type = "support",
                        description = "Heal 30% of max Hp for an ally. Require : Level 5",
                        hpGain = 0.3f,
                        mpGain = -3f,
                        isAtkBuff = false,
                        isDefBuff = false,
                        isMultiTarget = false,
                        isTargetEnemy = false,
                        remainTurn = 0,
                    },
                },
                unobtainedSkills = new CharacterDefaultSkill().witchUnobtained
            }
        };
        public Map mapData = new Map();
        public List<Item> items = new List<Item>()
        {
            new Item()
            {
                name = "Binh mau 1",
                description = "Hoi phuc 30% HP cho mot dong minh",
                amount = 5,
                atkPowGain = 0,
                consumable = true,
                defGain = 0,
                equippedOn = CharClass.noClass,
                couldBeUseOn = new List<CharClass>(){CharClass.healer, CharClass.warrior, CharClass.mage},
                HPGain = 0.3f,
                MPGain = 0,
                cost = 10
            },
            new Item()
            {
                name = "Binh mana 1",
                description = "Hoi phuc 30% MP cho mot dong minh",
                amount = 3,
                atkPowGain = 0,
                consumable = true,
                defGain = 0,
                equippedOn = CharClass.noClass,
                couldBeUseOn = new List<CharClass>(){CharClass.healer, CharClass.warrior, CharClass.mage},
                HPGain = 0,
                MPGain = 0.3f,
                cost = 10
            },
            new Item()
            {
                name = "Gang tay sat nam 1",
                description = "+44 Attack Power",
                amount = 1,
                atkPowGain = 44,
                consumable = false,
                defGain = 0,
                equippedOn = CharClass.warrior,
                couldBeUseOn = new List<CharClass>(){CharClass.warrior},
                HPGain = 0,
                MPGain = 0,
                cost = 6
            },
            new Item()
            {
                name = "gang tay sat nu 1",
                description = "+40 Attack Power",
                amount = 1,
                atkPowGain = 40,
                consumable = false,
                defGain = 0,
                equippedOn = CharClass.mage,
                couldBeUseOn = new List<CharClass>(){CharClass.healer, CharClass.mage},
                HPGain = 0,
                MPGain = 0,
                cost = 6
            },
            new Item()
            {
                name = "gang tay sat nu 1",
                description = "+40 Attack Power",
                amount = 1,
                atkPowGain = 40,
                consumable = false,
                defGain = 0,
                equippedOn = CharClass.healer,
                couldBeUseOn = new List<CharClass>(){CharClass.healer, CharClass.mage},
                HPGain = 0,
                MPGain = 0,
                cost = 6
            },
            new Item()
            {
                name = "giap nam 1",
                description = "+20 defend",
                amount = 1,
                atkPowGain = 0,
                consumable = false,
                defGain = 20,
                equippedOn = CharClass.mage,
                couldBeUseOn = new List<CharClass>(){CharClass.mage},
                HPGain = 0,
                MPGain = 0,
                cost = 10
            },
            new Item()
            {
                name = "giap nu 1",
                description = "+25 defend",
                amount = 1,
                atkPowGain = 0,
                consumable = false,
                defGain = 25,
                equippedOn = CharClass.mage,
                couldBeUseOn = new List<CharClass>(){CharClass.healer},
                HPGain = 0,
                MPGain = 0,
                cost = 20,
            }
        };
        public int Gold = 200;
        public DateTime saveTime = DateTime.Now;
    }
}
