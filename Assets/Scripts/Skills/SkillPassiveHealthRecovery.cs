using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Passive Health Recovery Skill", menuName = "Skills/Health Recovery Passive Skill")]
public class SkillPassiveHealthRecovery : Skill
{
	private SelfRecoveryTypes _selfRecoveryType;
	public SelfRecoveryTypes SelfRecoveryType
	{
		get { return _selfRecoveryType; }
		set { _selfRecoveryType = value; }
	}
	private RegenerationRate _regenerationRate;
	public RegenerationRate RegenerationRate 
	{ 
		get { return _regenerationRate; } 
		set { _regenerationRate = value; } 
	}
	public SkillPassiveHealthRecovery(string skillName, SelfRecoveryTypes selfRecoveryType, RegenerationRate regenerationRate, SkillType skillType = SkillType.Passive)
	{
		SkillName = skillName;
		_selfRecoveryType = selfRecoveryType;
		_regenerationRate = regenerationRate;
		SkillType = skillType;
		TargetType = Target.Self;
	}
}
