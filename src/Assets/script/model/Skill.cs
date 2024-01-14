using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.script.model
{
    public class Skill
    {
        public string name;
        public string description;
        public float mpCost;
        public float hpGain;
        public float mpGain;
        public bool isMultiTarget;
        public bool isAtkBuff;
        public bool isDefBuff;
        public int remainTurn;
        public string type;
        public float power;
        public bool isTargetEnemy;
    }
}
