using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.script.model
{
    public enum EnemyName
    {
        Slime,
        Orc,
        Harpy
    }

    public class EnemyModel
    {
        public int id;
        public EnemyName name;
        public bool isAlive;

    }
}
