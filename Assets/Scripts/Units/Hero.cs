using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hero : Unit
{
	private int _maxHealth = 0;
	private int _maxMana = 0;
	private int _currentStrength = 0;
	private int _currentMagic = 0;
	private int _currentSpeed = 0;
	private int _currentEndurance = 0;

	[SerializeField] int _playerSpeedAdvantage;
	[SerializeField] int _itemBonusStrength;
	[SerializeField] int _bonusArmor;
	[SerializeField] int _currentLevel;
	public int CurrentLevel
	{
		get { return _currentLevel; }
		set { _currentLevel = value;  }
	}

	private void Awake()
	{
		_maxHealth = ((_currentLevel - 1) * 8) + InitialHealth;
		CurrentHealth = _maxHealth;

		_maxMana = ((_currentLevel - 1) * 5) + InitialMana;
		CurrentMana = _maxMana;

		_currentStrength = InitialStrength;
		_currentEndurance = InitialEndurance;
		_currentMagic = InitialMagic;
		_currentSpeed = InitialSpeed;

		for (int i = 1; i <= _currentLevel; i++)
		{
			if (i % 2 == 0)
			{
				switch (UnitName)
				{
					case "Mercy":
						_currentStrength++;
						break;
					case "Inirius":
						_currentEndurance++;
						_currentSpeed++;
						break;
					case "Eni":
						_currentMagic++;
						break;
				}
			}
			else if (i % 2 != 0) 
			{
				switch (UnitName)
				{
					case "Mercy":
						_currentSpeed++;
						break;
					case "Inirius":
						break;
					case "Eni":
						_currentMagic++;
						break;
				}
				
			}
			else if (i % 3 == 0)
			{
				switch (UnitName)
				{
					case "Mercy":
						_currentMagic++;
						break;
					case "Inirius":
						_currentStrength++;
						break;
					case "Eni":
						_currentEndurance++;
						break;
				}
			}
			else if (!IsPrime(i))
			{
				switch (UnitName)
				{
					case "Mercy":
						_currentEndurance++;
						break;
					case "Inirius":
						_currentMagic++;
						break;
					case "Eni":
						_currentSpeed++;
						break;
				}
			}
		}
	}

	private int _currentActionValue 
	{ 
		get { return _currentActionValue; } 
		set { _currentActionValue = value; } 
	}

	public override void CalculateActionValue()
	{
		CurrentActionValue = (10000 / _currentSpeed) - _playerSpeedAdvantage;
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
		int receivedDamage = (int)(inputValue / Mathf.Sqrt(_currentEndurance * 8 + _bonusArmor));

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
			rawDamage = Mathf.Sqrt(0.5f * _itemBonusStrength) + Mathf.Sqrt(_currentStrength);
		}
		else if (attack == AttackType.Skill) {
			if (skillStat == 0)
			{
				throw new System.ArgumentException($"Skill power cannot be 0.");
			}
			rawDamage = Mathf.Sqrt(skillStat) + Mathf.Sqrt(_currentMagic);
		}
		else
		{
			throw new System.ArgumentException($"Invalid AttackType: {attack}. Currently available attack types (enum) are: AttackType.Normal, AttackType.Skill.");
		}

		output = (int)(rawDamage * isStrongerElemScale * isWeakerElemScale * isAttackBuffed * isEnemyDefUp);

		return output;
	}

	public static bool IsPrime(int number)
	{
		if (number < 2)
		{
			return false;
		}

		for (int i = 2; i <= Math.Sqrt(number); i++)
		{
			if (number % i == 0)
			{
				return false;
			}
		}

		return true;
	}
}
