using System.Collections.Generic;

namespace Assets.script.model
{
    public enum CharClass { 
        warrior,
        mage,
        healer,
        noClass
    }

    public class Character
    {
        public CharClass charClass = CharClass.warrior;
        public int level = 5;
        public int neededExp = 2;
        public int currExp = 0;
        public float maxHp = 300f;
        public float maxMp = 90f;
        public float currHp = 300f;
        public float currMp = 90f;
        public float atkPow = 20f;
        public float magic = 4;
        public float strength = 7f;
        public float speed = 3;
        public float endurance = 7;
        public float defStat = 20f;
        public string equippedWeapon;
        public string equippedArmor;
        public List<SkillModel> skills;
        public List<SkillModel> unobtainedSkills;
    }
}
