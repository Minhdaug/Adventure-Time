using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buff Skill", menuName = "Skills/Buff Skill")]
public class SkillBuff : Skill
{
	private Buffs _buffType;
	public Buffs BuffType 
	{ 
		get { return _buffType; } 
		set { _buffType = value; }
	}
	public SkillBuff(string skillName, AoE aoE, int manaCost, Buffs buff, SkillType skillType = SkillType.Active)
	{
		SkillName = skillName;
		AoE = aoE;
		ManaCost = manaCost;
		_buffType = buff;
		SkillType = skillType;
		TargetType = Target.Character;
	}
}
