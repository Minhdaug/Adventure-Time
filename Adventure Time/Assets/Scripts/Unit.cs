using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public enum UnitType
{
	Player, Enemy
}

public class Unit : MonoBehaviour
{
	private int currentActionValue;

	[SerializeField] string unitName;
	[SerializeField] UnitType unitType;
	[SerializeField] int level;
	[SerializeField] int damage;
	[SerializeField] int maxHP;
	[SerializeField] int currentHP;
	[SerializeField] int speed;
	[SerializeField] int playerAdvantage;
	[SerializeField] GameObject speedFrame;

	public void CalculateActionValue()
	{
		currentActionValue = 10000 / speed;

		if (unitType == UnitType.Player)
		{
			currentActionValue -= playerAdvantage;
		}
	}

	public int CurrentActionValue
	{
		get { return currentActionValue; }
	}

	public GameObject SpeedFrame { get { return speedFrame; } }

	public UnitType UnitType { get { return unitType; } }


	public void CalculateActionValue(int minActionValue)
	{
		currentActionValue -= minActionValue;
	}

	public void TakeDamage(int updateValue)
	{
		if (currentHP <= updateValue)
		{
			currentHP = 0;
		} 
		else
		{
			currentHP -= updateValue;
		}
	}
    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
