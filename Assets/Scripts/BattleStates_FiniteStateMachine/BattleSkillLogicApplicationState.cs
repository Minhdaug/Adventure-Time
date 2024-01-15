using System.Collections.Generic;
using UnityEngine;

public class BattleSkillLogicApplicationState : BattleBaseState
{
	private Unit _caster;
	private Skill _currentSkill;
	private Unit _target;

	private List<Hero> _heroesUnit = new List<Hero>();
	private List<Enemy> _enemiesUnit = new List<Enemy>();

	public readonly float _isStrongerElemScale = 1.6f;
	public readonly float _isWeakerElemScale = 0.6f;
	public readonly float _isAttackBuffed = 1.25f;
	public readonly float _isDefBuffed = 0.8f;

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
	public bool IsStrongerElem(ElementType caster, ElementType target)
	{
		return (strongerElemCircle[caster] == target);
	}
	public bool IsWeakerElem(ElementType caster, ElementType target)
	{
		return (weakerElemCircle[caster] == target);
	}
	public int HealValue(Unit target, HealthRecoveryRate healPercent)
	{
		int maxHealth, healPercentValue;
		if (target is Hero)
		{
			Hero target_hero = target as Hero;
			maxHealth = target_hero.MaxHealth;
		} 
		else
		{
			Enemy target_enemy = target as Enemy;
			maxHealth = target_enemy.InitialHealth;
		}
		
		if (healPercent == HealthRecoveryRate.Percentage30)
		{
			healPercentValue = 30;
		}
		else
		{
			healPercentValue = 50;
		}
		return (int)((maxHealth * healPercentValue) / 100);
	}
	public int PhysicalSkillDamageCalculator(Unit target, int rawDamage)
	{
		int targetEndurance;
		int targetBonusArmor;
		float attackBuffScale = 1.0f;
		float defenseBuffScale = 1.0f;
		
		if (target.IsDefenseBuff)
		{
			defenseBuffScale = _isDefBuffed;
		}
		if (_caster.IsAttackBuff)
		{
			attackBuffScale = _isAttackBuffed;
		}

		if (target is Hero)
		{
			Hero targetHero = target as Hero;

			targetEndurance = targetHero.CurrentEndurance;
			targetBonusArmor = targetHero.BonusArmor;
		}
		else
		{
			Enemy targetEnemy = target as Enemy;

			targetEndurance = targetEnemy.InitialEndurance;
			targetBonusArmor = 0;
		}

		return (int)((rawDamage/Mathf.Sqrt((targetEndurance * 8) + targetBonusArmor)) * attackBuffScale * defenseBuffScale);
	}
	public int MagicalSkillDamageCalculator(Unit target, int rawDamage)
	{
		int targetEndurance;
		int targetBonusArmor;
		float attackBuffScale = 1.0f;
		float defenseBuffScale = 1.0f;
		float isStrongerElemScale = 1.0f;
		float isWeakerElemScale = 1.0f;


		if (target.IsDefenseBuff)
		{
			defenseBuffScale = _isDefBuffed;
		}
		if (_caster.IsAttackBuff)
		{
			attackBuffScale = _isAttackBuffed;
		}
		if (IsStrongerElem(_caster.CharacterElemType, target.CharacterElemType))
		{
			isStrongerElemScale = _isStrongerElemScale;
		}
		if (IsWeakerElem(_caster.CharacterElemType, target.CharacterElemType))
		{
			isWeakerElemScale = _isWeakerElemScale;
		}

		if (target is Hero)
		{
			Hero targetHero = target as Hero;

			targetEndurance = targetHero.CurrentEndurance;
			targetBonusArmor = targetHero.BonusArmor;
		}
		else
		{
			Enemy targetEnemy = target as Enemy;

			targetEndurance = targetEnemy.InitialEndurance;
			targetBonusArmor = 0;
		}

		return (int)((rawDamage / Mathf.Sqrt((targetEndurance * 8) + targetBonusArmor)) * isStrongerElemScale * isWeakerElemScale * attackBuffScale * defenseBuffScale);
	}
	public override void EnterState(BattleStateManager state, Dictionary<int, bool> gameObjectId)
	{
		_caster = state.CurrentPlayer;
		_currentSkill = state.SelectedSkill;
		_target = state.SelectedTarget;

		Debug.Log("Entered SkillLogicApplicationState");
		Debug.Log($"_caster: {_caster.UnitName}");
		Debug.Log($"_currentSkill: {_currentSkill.SkillName}");
		Debug.Log($"_target: {_target.UnitName}");

		for (int i = 0; i < state.HeroesGO.Count; i++)
		{
			Hero tmpUnit = state.HeroesGO[i].GetComponent<Unit>() as Hero;

			_heroesUnit.Add(tmpUnit);
		}
		for (int i = 0; i < state.EnemiesGO.Count; i++)
		{
			Enemy tmpUnit = state.EnemiesGO[i].GetComponent<Unit>() as Enemy;

			_enemiesUnit.Add(tmpUnit);
		}
		UpdateState(state);
	}

	public override void UpdateState(BattleStateManager state)
	{
		Debug.Log("Entered SkillLogicApplicationState - Update State");

		if (_currentSkill is SkillBuff)
		{
			Debug.Log("Skill is SkillBuff");
			SkillBuff currentSkill = _currentSkill as SkillBuff;
			Debug.Log("currentSkill info:");
			Debug.Log($"BuffType: {currentSkill.BuffType}");
			Debug.Log($"SkillName: {currentSkill.SkillName}");
			Debug.Log($"AoE: {currentSkill.AoE}");

			if (currentSkill.AoE == AoE.One)
			{
				if (currentSkill.BuffType == Buffs.Attack)
				{
					_target.AttackBuffCount = 3;
					_target.IsAttackBuff = true;

					Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on {_target.UnitName}. {_target.UnitName} has gained {_currentSkill.SkillName} for {_target.AttackBuffCount} turn.");
				}
				else if (currentSkill.BuffType == Buffs.Defense)
				{
					_target.DefenseBuffCount = 3;
					_target.IsDefenseBuff = true;
				}
			} 
			else if (currentSkill.AoE == AoE.All)
			{
				Debug.Log("Skill is AoE.All");
				if (currentSkill.BuffType == Buffs.Attack)
				{
					Debug.Log("Skill is Attack Buff");
					if (_target is Hero)
					{
						Debug.Log("Target is Hero");
						for (int i = 0; i < _heroesUnit.Count; i++)
						{
							_heroesUnit[i].IsAttackBuff = true;
							_heroesUnit[i].AttackBuffCount = 3;
						}

						Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on teammates. All heroes gained {_currentSkill.SkillName} for {_target.AttackBuffCount} turn.");
					}
					else if (_target is Enemy)
					{
						Debug.Log("Target is Enemy");
						for (int i = 0; i < _enemiesUnit.Count; i++)
						{
							_enemiesUnit[i].IsAttackBuff = true;
							_enemiesUnit[i].AttackBuffCount = 3;
						}

						Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on teammates. All enemies gained {_currentSkill.SkillName} for {_target.AttackBuffCount} turn.");
					}
				}
				else if (currentSkill.BuffType == Buffs.Defense)
				{
					if (_target is Hero)
					{
						Debug.Log("Target is Hero");
						for (int i = 0; i < _heroesUnit.Count; i++)
						{
							_heroesUnit[i].IsDefenseBuff = true;
							_heroesUnit[i].DefenseBuffCount = 3;
						}

						Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on teammates. All heroes gained {_currentSkill.SkillName} for {_target.AttackBuffCount} turn.");
					}
					else if (_target is Enemy)
					{
						Debug.Log("Target is Enemy");
						for (int i = 0; i < _enemiesUnit.Count; i++)
						{
							_enemiesUnit[i].IsDefenseBuff = true;
							_enemiesUnit[i].DefenseBuffCount = 3;
						}

						Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on teammates. All enemies gained {_currentSkill.SkillName} for {_target.AttackBuffCount} turn.");
					}
				}
			}
		}
		else if (_currentSkill is SkillHeal)
		{
			SkillHeal currentSkill = _currentSkill as SkillHeal;

			if (currentSkill.AoE == AoE.One)
			{
				int healValue = HealValue(_target, currentSkill.HealthRecoveryRate);

				_target.Heal(healValue);

				Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on {_target.UnitName}. {_target.UnitName} for {healValue} HP.");
			}
			else if (currentSkill.AoE != AoE.All)
			{
				if (_target is Hero)
				{
					for (int i = 0; i < _heroesUnit.Count; i++)
					{
						_heroesUnit[i].Heal(HealValue(_heroesUnit[i], currentSkill.HealthRecoveryRate));

						Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on teammates. All heroes healed {_currentSkill.SkillName} for {_target.AttackBuffCount} turn.");
					}
				}
				else if (_target is Enemy)
				{
					for (int i = 0; i < _enemiesUnit.Count; i++)
					{
						_enemiesUnit[i].Heal(HealValue(_enemiesUnit[i], currentSkill.HealthRecoveryRate));

						Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on teammates. All enemies gained {_currentSkill.SkillName} for {_target.AttackBuffCount} turn.");
					}
				}
			}
		}
		else if (_currentSkill is SkillPhysical)
		{
			SkillPhysical currentSkill = _currentSkill as SkillPhysical;

			if (currentSkill.AoE == AoE.One)
			{
				int rawDamage = _caster.PhysicalDamageOutput(currentSkill);
				int calculatedDamage = PhysicalSkillDamageCalculator(_target, rawDamage);
				_target.TakeDamage(calculatedDamage);

				Debug.Log($"Target: {_target.UnitName}; Target HP: {_target}");
				Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on {_target.UnitName}. All enemies gained {_currentSkill.SkillName} for {_target.AttackBuffCount} turn.");
			}
			else if (currentSkill.AoE == AoE.All)
			{
				int rawDamage = _caster.PhysicalDamageOutput(currentSkill);

				Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on all heroes.");

				if (_target is Hero)
				{
					for (int i = 0; i < _heroesUnit.Count; i++)
					{
						int calculatedDamage = PhysicalSkillDamageCalculator(_heroesUnit[i], rawDamage);
						_heroesUnit[i].TakeDamage(calculatedDamage);

						Debug.Log($"Target: {_heroesUnit[i].UnitName}; Target HP: {_heroesUnit[i]}");
						Debug.Log($"{_heroesUnit[i]} received {calculatedDamage}.");
					}

					 //All enemies gained {_currentSkill.SkillName} for {_target.AttackBuffCount} turn.");
				}
				else
				{
					for (int i = 0; i < _enemiesUnit.Count; i++)
					{
						int calculatedDamage = PhysicalSkillDamageCalculator(_enemiesUnit[i], rawDamage);
						_enemiesUnit[i].TakeDamage(calculatedDamage);

						Debug.Log($"Target: {_enemiesUnit[i].UnitName}; Target HP: {_enemiesUnit[i]}");
						Debug.Log($"{_enemiesUnit[i]} received {calculatedDamage}.");
					}
				}
			}
		}
		else if (_currentSkill is NormalAttack)
		{
			int rawDamage = _caster.NormalDamageOutput();

			int strength;

			if (_caster is Hero)
			{
				Hero hero = (Hero)_caster;
				strength = hero.CurrentStrength;
			}
			else
			{
				Enemy enemy = (Enemy)_caster;
				strength = enemy.InitialStrength;
			}
			Debug.Log($"NormalAttack rawDamage: {rawDamage}");
			Debug.Log($"currentStrength: {strength}");
			Debug.Log($"initialStrength: {_caster.InitialStrength}");
			int calculatedDamage = PhysicalSkillDamageCalculator(_target, rawDamage);
			_target.TakeDamage(calculatedDamage);

			Debug.Log($"Target: {_target.UnitName}; Target HP: {_target.CurrentHealth}");
			Debug.Log($"{_target.UnitName} received {calculatedDamage}.");

		}
		else if (_currentSkill is SkillMagical)
		{
			SkillMagical currentSkill = _currentSkill as SkillMagical;

			if (currentSkill.AoE == AoE.One)
			{
				int rawDamage = _caster.MagicalDamageOutput(_target.CharacterElemType, currentSkill);
				int calculatedDamage = MagicalSkillDamageCalculator(_target, rawDamage);
				_target.TakeDamage(calculatedDamage);
			}
			else if (currentSkill.AoE == AoE.All)
			{
				if (_target is Hero)
				{
					for (int i = 0; i < _heroesUnit.Count; i++)
					{
						int rawDamage = _caster.MagicalDamageOutput(_heroesUnit[i].CharacterElemType, currentSkill);
						int calculatedDamage = MagicalSkillDamageCalculator(_heroesUnit[i], rawDamage);
						_heroesUnit[i].TakeDamage(calculatedDamage);
					}
				}
				else
				{
					for (int i = 0; i < _enemiesUnit.Count; i++)
					{
						int rawDamage = _caster.MagicalDamageOutput(_enemiesUnit[i].CharacterElemType, currentSkill);
						int calculatedDamage = MagicalSkillDamageCalculator(_enemiesUnit[i], rawDamage);
						_enemiesUnit[i].TakeDamage(calculatedDamage);
					}
				}
			}
		}


	}
}
