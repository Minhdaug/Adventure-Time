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
	protected int CurrentHealth { 
		get { return _currentHealth; }
		set { _currentHealth = value; }
	}
	[SerializeField] int _initialHealth;
	protected int InitialHealth
	{
		get { return _initialHealth; }
		set { _initialHealth = value; }
	}
	private int _currentMana;
	protected int CurrentMana
	{
		get { return _currentMana; }
		set { _currentMana = value; }
	}
	[SerializeField] int _initialMana;	
	protected int InitialMana
	{
		get { return _initialMana; }
		set { _initialMana = value; }
	}
	[SerializeField] int _initialSpeed;
	protected int InitialSpeed
	{
		get { return _initialSpeed; } 
		set { _initialSpeed = value; }
	}
	[SerializeField] int _initialEndurance;
	protected int InitialEndurance
	{
		get { return _initialEndurance; }
		set { _initialEndurance = value; }
	}
	[SerializeField] int _initialStrength;
	protected int InitialStrength
	{
		get { return _initialStrength; }
		set { _initialStrength = value; }
	}
	[SerializeField] int _initialMagic;
	protected int InitialMagic
	{
		get { return _initialMagic; }
		set { _initialMagic = value; }
	}
	[SerializeField] ElementType _characterElemType;
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

	public readonly float _isStrongerElemScale = 1.6f;
	public readonly float _isWeakerElemScale = 0.6f;
	public readonly float _isAttackBuffed = 1.25f;
	public readonly float _isEnemyDefUp = 0.8f;
	
	public readonly Dictionary<ElementType, ElementType> strongerElemCircle = new Dictionary<ElementType, ElementType>() 
	{
		{ ElementType.Fire, ElementType.Poison },
			{ ElementType.Poison, ElementType.Water },
			{ ElementType.Water, ElementType.Fire }
	};
	public readonly Dictionary<ElementType, ElementType> weakerElemCircle = new Dictionary<ElementType, ElementType>() 
	{
		{ ElementType.Poison, ElementType.Fire },
			{ ElementType.Water, ElementType.Poison },
			{ ElementType.Fire, ElementType.Water }
	};
	
	public abstract void CalculateActionValue();
	public abstract int DamageOutput(AttackType attack, ElementType target, int skillStat = 0);
	public abstract void TakeDamage(int updateValue);
	public abstract void ManaConsume(int updateValue);
	public void UpdateActionValue(int updateValue)
	{
		_currentActionValue -= updateValue;
	}
	public bool IsStrongerElem(ElementType type)
	{
		return (strongerElemCircle[_characterElemType] == type);
	}
	public bool IsWeakerElem(ElementType type)
	{
		return (weakerElemCircle[_characterElemType] == type);
	}
	private void Awake()
	{
		_currentHealth = _initialHealth;
		_currentMana = _initialMana;
	}
}
