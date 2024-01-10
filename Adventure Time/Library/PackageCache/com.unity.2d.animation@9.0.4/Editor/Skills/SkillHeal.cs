using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Heal Skill", menuName = "Skills/Heal Skill")]
public class SkillHeal : Skill
{
	private HealthRecoveryRate _healthRecoveryRate;
	public HealthRecoveryRate HealthRecoveryRate
	{
		get { return _healthRecoveryRate; }
		set { _healthRecoveryRate = value; }
	}
	public SkillHeal(string skillName, AoE aoE, int manaCost, HealthRecoveryRate HealthRecoveryRate,  SkillType skillType = SkillType.Active)
	{
		SkillName = skillName;
		AoE = aoE;
		ManaCost = manaCost;
		_healthRecoveryRate = HealthRecoveryRate;
		SkillType = skillType;
		TargetType = Target.Character;
	}
}
