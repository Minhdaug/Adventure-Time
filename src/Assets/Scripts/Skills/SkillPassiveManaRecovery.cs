using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Passive Mana Recovery Skill", menuName = "Skills/Mana Recovery Passive Skill")]
public class SkillPassiveManaRecovery : Skill
{
	private SelfRecoveryTypes _selfRecoveryType;
	public SelfRecoveryTypes SelfRecoveryType
	{
		get { return _selfRecoveryType; }
		set { _selfRecoveryType = value; }
	}
	private ManaRecoveryRate _manaRecoveryRate;
	public ManaRecoveryRate ManaRecoveryRate
	{
		get { return _manaRecoveryRate; }
		set { _manaRecoveryRate = value;}
	}
	public SkillPassiveManaRecovery(string skillName, SelfRecoveryTypes selfRecoveryType, ManaRecoveryRate manaRecoveryRate, SkillType skillType = SkillType.Passive)
	{
		SkillName = skillName;
		_selfRecoveryType = selfRecoveryType;
		_manaRecoveryRate = manaRecoveryRate;
		SkillType = skillType;
		TargetType = Target.Self;
	}
}
