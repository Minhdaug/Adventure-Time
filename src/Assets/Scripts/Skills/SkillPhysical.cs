using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Physical Skill", menuName = "Skills/Physical Skill")]
public class SkillPhysical : Skill
{
    private int _skillStat;
    public int SkillStat
    {
        get { return _skillStat; }
        set { _skillStat = value; }
    }
}
