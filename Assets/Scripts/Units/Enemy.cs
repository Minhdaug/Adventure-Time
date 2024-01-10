using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
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

	public override void TakeDamage(int inputValue)
	{
		int receivedDamage = (int)(inputValue / Mathf.Sqrt(InitialEndurance * 8));

		if (CurrentHealth <= receivedDamage)
		{
			CurrentHealth = 0;
		}
		else
		{
			CurrentHealth -= receivedDamage;
		}
	}

	public override int DamageOutput(AttackType attack, ElementType target, int skillStat = 0)
	{
		int output;
		float rawDamage;

		float isStrongerElemScale = 1.0f;
		float isWeakerElemScale = 1.0f;
		float isAttackBuffed = 1.0f;
		float isEnemyDefUp = 1.0f;

		if (IsStrongerElem(target))
		{
			isStrongerElemScale = _isStrongerElemScale;
		}
		if (IsStrongerElem(target))
		{
			isWeakerElemScale = _isWeakerElemScale;
		}
		if (IsStrongerElem(target))
		{
			isAttackBuffed = _isAttackBuffed;
		}
		if (IsStrongerElem(target))
		{
			isEnemyDefUp = _isEnemyDefUp;
		}

		if (attack == AttackType.Normal)
		{
			rawDamage = Mathf.Sqrt(0.5f) + Mathf.Sqrt(InitialStrength);
		}
		else if (attack == AttackType.Skill)
		{
			if (skillStat == 0)
			{
				throw new System.ArgumentException($"Skill power cannot be 0.");
			}
			rawDamage = Mathf.Sqrt(skillStat) + Mathf.Sqrt(InitialMagic);
		}
		else
		{
			throw new System.ArgumentException($"Invalid AttackType: {attack}. Currently available attack types (enum) are: AttackType.Normal, AttackType.Skill.");
		}

		output = (int)(rawDamage * isStrongerElemScale * isWeakerElemScale * isAttackBuffed * isEnemyDefUp);

		return output;
	}
}
