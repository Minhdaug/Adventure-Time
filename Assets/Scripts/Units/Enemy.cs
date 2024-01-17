using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
	public void Awake()
	{
		CurrentHealth = InitialHealth;
		CurrentMana = InitialHealth;
	}

	private int _currentActionValue
	{
		get { return _currentActionValue; }
		set { _currentActionValue = value; }
	}

	public override void CalculateActionValue()
	{
		CurrentActionValue = (10000 / InitialSpeed);
	}

	public override void ManaConsume(int updateValue)
	{
		if (CurrentMana <= updateValue)
		{
			CurrentMana = 0;
		}
		else
		{
			CurrentMana -= updateValue;
		}
	}

	public override void Heal(int updateValue)
	{
		if (CurrentHealth + updateValue > InitialHealth)
		{
			CurrentHealth = InitialHealth;
		}
		else
		{
			CurrentHealth += updateValue;
		}
	}

	public override void TakeDamage(int inputValue)
	{
		if (CurrentHealth <= inputValue)
		{
			CurrentHealth = 0;
		}
		else
		{
			CurrentHealth -= inputValue;
		}
	}

	public override int NormalDamageOutput()
	{
		//int rawDamage = (int)(Mathf.Sqrt(0.5f) + Mathf.Sqrt(InitialStrength));
		return InitialStrength;
		//return rawDamage;
	}

	public override int PhysicalDamageOutput(SkillPhysical skill)
	{
		//return (int)(Mathf.Sqrt(skill.SkillStat) + Mathf.Sqrt(InitialStrength));
		return (int)(skill.SkillStat + InitialStrength);
	}

	public override int MagicalDamageOutput(ElementType target, SkillMagical skill)
	{
		return (int)(skill.SkillStat + InitialMagic);
		//return (int)(Mathf.Sqrt(skill.SkillStat) + Mathf.Sqrt(InitialMagic));
	}
}
