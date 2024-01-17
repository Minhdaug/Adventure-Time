using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFire : SkillMagical
{
	public SkillFire(string skillName, AoE aoE, ElementType elementType, int skillStat, int manaCost, SkillType skillType = SkillType.Active) : base(skillName, aoE, elementType, skillStat, manaCost, skillType)
	{
	}
}
