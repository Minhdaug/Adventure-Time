using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Magical Skill", menuName = "Skills/Magical Skill")]
public class SkillMagical : Skill
{
	private ElementType _elementType;
	public ElementType elementType
	{
		get { return _elementType; }
		set { _elementType = value; }
	}
	private int _skillStat;
	public int SkillStat
	{
		get { return (int)_skillStat; }
		set { _skillStat = value; }
	}
	public SkillMagical(string skillName, AoE aoE, ElementType elementType, int skillStat, int manaCost, SkillType skillType = SkillType.Active)
	{
		SkillName = skillName;
		AoE = aoE;
		_elementType = elementType;
		_skillStat = skillStat;
		DamageType = DamageType.Magical;
		ManaCost = manaCost;
		SkillType = skillType;
		TargetType = Target.Enemy;
	}
}
