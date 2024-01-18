using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.script.model
{
    public class CombatData
    {
        public Dictionary<string, int> characterLvl = new Dictionary<string, int>
        {
            {"Mercy", 5 },
            {"Inirius", 5 },
            {"Eni", 5 },
        };
        public List<string> enemiesInAct;
    }
}
