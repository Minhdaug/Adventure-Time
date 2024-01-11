using System.Collections.Generic;

namespace Assets.script.model
{
    public enum CharClass { 
        warrior,
        mage,
        healer
    }

    public class Character
    {
        public CharClass charClass = CharClass.warrior;
        public float maxHp = 300f;
        public float maxMp = 90f;
        public float currHp = 300f;
        public float currMp = 90f;
        public float atkPow = 20f;
        public float magPow = 15;
        public float defStat = 20f;
        public string equippedWeapon;
        public string equippedArmor;
        public List<string> skills;
    }
}
