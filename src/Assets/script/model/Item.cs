using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.script.model
{
    public class Item
    {
        public string name;
        public string description;
        public bool consumable;
        public int amount;
        public float HPGain;
        public float MPGain;
        public float atkPowGain;
        public float defGain;
        public CharClass equippedOn;
        public List<CharClass> couldBeUseOn;
        public int cost;
    }
}
