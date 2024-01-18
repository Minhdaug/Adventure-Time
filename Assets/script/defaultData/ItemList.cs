using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Assets.script.model;
using Unity.VisualScripting;

namespace Assets.script.defaultData
{
    public class ItemListData
    {
        public List<Item> ShopItems = new()
        {
            new Item()
            {
                name = "Potion 1",
                description = "Heal 30% Max Hp for an ally",
                amount = 1,
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
                name = "Potion 2",
                description = "Heal 50% Max Hp for an ally",
                amount = 1,
                atkPowGain = 0,
                consumable = true,
                defGain = 0,
                equippedOn = CharClass.noClass,
                couldBeUseOn = new List<CharClass>(){CharClass.healer, CharClass.warrior, CharClass.mage},
                HPGain = 0.5f,
                MPGain = 0,
                cost = 20
            },
            new Item()
            {
                name = "Energy Drink 1",
                description = "Recover 30% Max Mp for an ally",
                amount = 1,
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
                name = "Energy Drink 2",
                description = "Recover 50% Max Mp for an ally",
                amount = 1,
                atkPowGain = 0,
                consumable = true,
                defGain = 0,
                equippedOn = CharClass.noClass,
                couldBeUseOn = new List<CharClass>(){CharClass.healer, CharClass.warrior, CharClass.mage},
                HPGain = 0,
                MPGain = 0.5f,
                cost = 20
            },
            new Item()
            {
                name = "Warrior Sword 2",
                description = "+54 Attack Power",
                amount = 1,
                atkPowGain = 54,
                consumable = false,
                defGain = 0,
                equippedOn = CharClass.noClass,
                couldBeUseOn = new List<CharClass>(){CharClass.warrior},
                HPGain = 0,
                MPGain = 0,
                cost = 20
            },
            new Item()
            {
                name = "Warrior Sword 3",
                description = "+64 Attack Power",
                amount = 1,
                atkPowGain = 64,
                consumable = false,
                defGain = 0,
                equippedOn = CharClass.noClass,
                couldBeUseOn = new List<CharClass>(){CharClass.warrior},
                HPGain = 0,
                MPGain = 0,
                cost = 40
            },
            new Item()
            {
                name = "Warrior Sword 4",
                description = "+74 Attack Power",
                amount = 1,
                atkPowGain = 74,
                consumable = false,
                defGain = 0,
                equippedOn = CharClass.noClass,
                couldBeUseOn = new List<CharClass>(){CharClass.warrior},
                HPGain = 0,
                MPGain = 0,
                cost = 80
            },
            new Item()
            {
                name = "Spellcaster Scepter 2",
                description = "+50 Attack Power",
                amount = 1,
                atkPowGain = 50,
                consumable = false,
                defGain = 0,
                equippedOn = CharClass.noClass,
                couldBeUseOn = new List<CharClass>(){CharClass.healer, CharClass.mage},
                HPGain = 0,
                MPGain = 0,
                cost = 20
            },
            new Item()
            {
                name = "Spellcaster Scepter 3",
                description = "+60 Attack Power",
                amount = 1,
                atkPowGain = 60,
                consumable = false,
                defGain = 0,
                equippedOn = CharClass.noClass,
                couldBeUseOn = new List<CharClass>(){CharClass.healer, CharClass.mage},
                HPGain = 0,
                MPGain = 0,
                cost = 40
            },
            new Item()
            {
                name = "Spellcaster Scepter 4",
                description = "+70 Attack Power",
                amount = 1,
                atkPowGain = 70,
                consumable = false,
                defGain = 0,
                equippedOn = CharClass.noClass,
                couldBeUseOn = new List<CharClass>(){CharClass.healer, CharClass.mage},
                HPGain = 0,
                MPGain = 0,
                cost = 80
            },
            new Item()
            {
                name = "Warrior Armor 2",
                description = "+40 defend",
                amount = 1,
                atkPowGain = 0,
                consumable = false,
                defGain = 40,
                equippedOn = CharClass.noClass,
                couldBeUseOn = new List<CharClass>(){CharClass.warrior},
                HPGain = 0,
                MPGain = 0,
                cost = 20
            },
            new Item()
            {
                name = "Warrior Armor 3",
                description = "+50 defend",
                amount = 1,
                atkPowGain = 0,
                consumable = false,
                defGain = 50,
                equippedOn = CharClass.noClass,
                couldBeUseOn = new List<CharClass>(){CharClass.warrior},
                HPGain = 0,
                MPGain = 0,
                cost = 40
            },
            new Item()
            {
                name = "Warrior Armor 4",
                description = "+60 defend",
                amount = 1,
                atkPowGain = 0,
                consumable = false,
                defGain = 60,
                equippedOn = CharClass.noClass,
                couldBeUseOn = new List<CharClass>(){CharClass.warrior},
                HPGain = 0,
                MPGain = 0,
                cost = 80
            },new Item()
            {
                name = "Warrior Armor 5",
                description = "+75 defend",
                amount = 1,
                atkPowGain = 0,
                consumable = false,
                defGain = 75,
                equippedOn = CharClass.noClass,
                couldBeUseOn = new List<CharClass>(){CharClass.warrior},
                HPGain = 0,
                MPGain = 0,
                cost = 120
            },
            new Item()
            {
                name = "Spellcaster Armor 2",
                description = "+40 defend",
                amount = 1,
                atkPowGain = 0,
                consumable = false,
                defGain = 40,
                equippedOn = CharClass.noClass,
                couldBeUseOn = new List<CharClass>(){CharClass.mage},
                HPGain = 0,
                MPGain = 0,
                cost = 20
            },
            new Item()
            {
                name = "Spellcaster Armor 3",
                description = "+50 defend",
                amount = 1,
                atkPowGain = 0,
                consumable = false,
                defGain = 50,
                equippedOn = CharClass.noClass,
                couldBeUseOn = new List<CharClass>(){CharClass.mage},
                HPGain = 0,
                MPGain = 0,
                cost = 40
            },
            new Item()
            {
                name = "Spellcaster Armor 4",
                description = "+60 defend",
                amount = 1,
                atkPowGain = 0,
                consumable = false,
                defGain = 60,
                equippedOn = CharClass.noClass,
                couldBeUseOn = new List<CharClass>(){CharClass.mage},
                HPGain = 0,
                MPGain = 0,
                cost = 80
            },new Item()
            {
                name = "Spellcaster Armor 5",
                description = "+75 defend",
                amount = 1,
                atkPowGain = 0,
                consumable = false,
                defGain = 75,
                equippedOn = CharClass.noClass,
                couldBeUseOn = new List<CharClass>(){CharClass.mage},
                HPGain = 0,
                MPGain = 0,
                cost = 120
            },
        };
    }
}
