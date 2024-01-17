using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ElementType
{
	Fire, Water, Poison, Neutral
}
public enum AttackType
{
	Skill, Normal
}

public abstract class Unit : MonoBehaviour
{
	[SerializeField] string _unitName;
	public string UnitName
	{
		get { return _unitName; }
		set { _unitName = value; }
	}
	private int _currentHealth;
	public int CurrentHealth { 
		get { return _currentHealth; }
		set { _currentHealth = value; }
	}
	[SerializeField] int _initialHealth;
	public int InitialHealth
	{
		get { return _initialHealth; }
		set { _initialHealth = value; }
	}
	private int _currentMana;
	public int CurrentMana
	{
		get { return _currentMana; }
		set { _currentMana = value; }
	}
	[SerializeField] int _initialMana;	
	public int InitialMana
	{
		get { return _initialMana; }
		set { _initialMana = value; }
	}
	[SerializeField] int _initialSpeed;
	public int InitialSpeed
	{
		get { return _initialSpeed; } 
		set { _initialSpeed = value; }
	}
	[SerializeField] int _initialEndurance;
	public int InitialEndurance
	{
		get { return _initialEndurance; }
		private set { _initialEndurance = value; }
	}
	[SerializeField] int _initialStrength;
	public int InitialStrength
	{
		get { return _initialStrength; }
		set { _initialStrength = value; }
	}
	[SerializeField] int _initialMagic;
	public int InitialMagic
	{
		get { return _initialMagic; }
		set { _initialMagic = value; }
	}
	[SerializeField] ElementType _characterElemType;
	public ElementType CharacterElemType
	{
		get { return _characterElemType; }
		set	{ _characterElemType = value; }
	}

	private int _currentActionValue;
	public int CurrentActionValue 
	{ 
		get { return _currentActionValue;} 
		set { _currentActionValue = value; } 
	}
	[SerializeField] GameObject _selectionFrame;
	public GameObject SelectionFrame
	{
		get { return _selectionFrame; }
		set { _selectionFrame = value; }
	}

	[SerializeField] GameObject _speedFrame;
	public GameObject SpeedFrame
	{
		get { return _speedFrame; }
		set { _speedFrame = value; }
	}

	[SerializeField] bool _isAttackBuff;
	public bool IsAttackBuff
	{
		get { return _isAttackBuff; }
		set { _isAttackBuff = value; }
	}
	[SerializeField] int _attackBuffCount = 0;
	public int AttackBuffCount
	{
		get { return _attackBuffCount; }
		set { _attackBuffCount = value; }
	}

	[SerializeField] bool _isDefenseBuff;
	public bool IsDefenseBuff
	{
		get { return _isDefenseBuff; }
		set { _isDefenseBuff = value; }
	}
	[SerializeField] int _defenseBuffCount = 0;
	public int DefenseBuffCount
	{
		get { return _defenseBuffCount; }
		set { _defenseBuffCount = value; }
	}

	public void UpdateBuff()
	{
		if (_attackBuffCount > 0) 
		{
			_attackBuffCount--;
		}
		if (_defenseBuffCount > 0)
		{
			_defenseBuffCount--;
		}
	}

	public abstract void CalculateActionValue();
	public abstract int NormalDamageOutput();
	public abstract int PhysicalDamageOutput(SkillPhysical skill);
	public abstract int MagicalDamageOutput(ElementType target, SkillMagical skill);
	public abstract void TakeDamage(int updateValue);
	public abstract void Heal(int updateValue);
	public abstract void ManaConsume(int updateValue);

	public void ManaRecovery(int updateValue)
	{
		if (CurrentMana + updateValue > _initialMana)
		{
			CurrentMana = _initialMana;
		}
		else
		{
			CurrentMana += updateValue;
		}
	}
	public void UpdateActionValue(int updateValue)
	{
		_currentActionValue -= updateValue;
	}
	private void Awake()
	{
		_currentHealth = _initialHealth;
		_currentMana = _initialMana;
	}
	public void DisableOnDeath()
	{
		gameObject.SetActive(false);
	}
}
