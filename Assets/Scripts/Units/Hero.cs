using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Hero : Unit
{
	private int _maxHealth = 0;
	public int MaxHealth 
	{ 
		get { return _maxHealth; } 
	}

	private int _maxMana = 0;

	private int _currentStrength = 0;
	public int CurrentStrength
	{
		get { return _currentStrength; }
		set { _currentStrength = value; }
	}

	private int _currentMagic = 0;

	private int _currentSpeed = 0;

	private int _currentEndurance = 0;

	public int CurrentEndurance
	{
		get { return (int)_currentEndurance; }
	}

	[SerializeField] int _playerSpeedAdvantage;
	[SerializeField] int _itemBonusStrength;
	[SerializeField] int _bonusArmor;
	public int BonusArmor
	{
		get { return _bonusArmor; }
		set { _bonusArmor = value; }
	}
	[SerializeField] int _currentLevel;
	public int CurrentLevel
	{
		get { return _currentLevel; }
		set { _currentLevel = value;  }
	}

	public void Awake()
	{
		Debug.Log("Awake of Hero.cs called");

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
			if (i % 2 != 0) 
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
			if (i % 3 == 0)
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
			if (!IsPrime(i))
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

		Debug.Log($"_currentStrength: {_currentStrength}");
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
		if (CurrentHealth <= inputValue)
		{
			CurrentHealth = 0;
		}
		else
		{
			CurrentHealth -= inputValue;
		}
	}

	public override void Heal(int updateValue)
	{
		if (CurrentHealth + updateValue > _maxHealth)
		{
			CurrentHealth = _maxHealth;
		}
		else
		{
			CurrentHealth += updateValue;
		}
	}

	public override int NormalDamageOutput()
	{
		int rawDamage = (int)(Mathf.Sqrt(0.5f * _itemBonusStrength) + Mathf.Sqrt(_currentStrength));
		return rawDamage;
	}

	public override int PhysicalDamageOutput(SkillPhysical skill)
	{
		return (int)(Mathf.Sqrt(skill.SkillStat) + Mathf.Sqrt(_currentStrength));
	}

	public override int MagicalDamageOutput(ElementType target, SkillMagical skill)
	{
		return (int)(Mathf.Sqrt(skill.SkillStat) + Mathf.Sqrt(_currentMagic));
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
