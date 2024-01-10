using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Target
{
	Character, Enemy, Self
}
public enum SkillType
{
	Active, Passive
}
public enum DamageType 
{ 
    Physical, Magical, Support
}
public enum AoE
{
    All, One
}

public enum Buffs
{
    Attack, Defense
}

public enum HealthRecoveryRate
{
	Percentage30, Percentage50
}

public enum RegenerationRate
{
	Percentage2, Percentage4
}

public enum ManaRecoveryRate
{
    ManaUnit3, ManaUnit5
}

public enum SelfRecoveryTypes
{
	Health, Mana
}

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/Skill")]
public class Skill : ScriptableObject
{
    private string _skillName;
    public string SkillName
    {
        get { return _skillName; }
        set { _skillName = value; }
	}

	private AoE _aoe;
	public AoE AoE
	{
		get { return _aoe; }
		set { _aoe = value; }
	}

	private DamageType _damageType;
	public DamageType DamageType
	{
		get { return _damageType; }
		set { _damageType = value; }
	}

	private int _manaCost;
	public int ManaCost
	{
		get { return _manaCost; }
		set { _manaCost = value; }
	}

	private SkillType _skillType;
	public SkillType SkillType 
	{
		get { return _skillType; }
		set { _skillType = value; }
	}

	private Target _targetType;
	public Target TargetType 
	{ 
		get { return _targetType; } 
		set { _targetType = value; }
	}
}
