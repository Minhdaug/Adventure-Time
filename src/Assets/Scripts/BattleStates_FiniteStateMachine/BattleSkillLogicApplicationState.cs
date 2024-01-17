using System.Collections.Generic;
using UnityEngine;

public class BattleSkillLogicApplicationState : BattleBaseState
{
	private Unit _caster;
	private Skill _currentSkill;
	private Unit _target;
	private bool _recentlyCastAttackBuff = false;
	private bool _recentlyCastDefenseBuff = false;
	private Dictionary<string, Animator> _unitAnimator;

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
	public int HealValue(Unit target, RegenerationRate healPercent)
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

		if (healPercent == RegenerationRate.Percentage2)
		{
			healPercentValue = 2;
		}
		else
		{
			healPercentValue = 4;
		}
		return (int)((maxHealth * healPercentValue) / 100);
	}
	public void ManaRecover(ManaRecoveryRate manaRecoveryRate)
	{
		int manaRecoveryValue;
		int maxMana;
		int percent;

		if (_caster is Hero)
		{
			Hero hero = _caster as Hero;
			maxMana = hero.MaxMana;
		}
		else
		{
			Enemy enemy = _caster as Enemy;
			maxMana = enemy.InitialMana;
		}

		if (manaRecoveryRate == ManaRecoveryRate.ManaUnit3)
		{
			percent = 3;
		}
		else
		{
			percent = 5;
		}

		manaRecoveryValue = (int)((maxMana * percent) / 100);
		_caster.ManaRecovery(manaRecoveryValue);
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

		//Debug.Log($"(rawDamage/Mathf.Sqrt((targetEndurance * 8) + targetBonusArmor)): {(rawDamage / Mathf.Sqrt((targetEndurance * 8) + targetBonusArmor))}");

		int calculatedOutput = (int)((rawDamage / Mathf.Sqrt((targetEndurance * 8) + targetBonusArmor)) * attackBuffScale * defenseBuffScale);

		if ( calculatedOutput <= 0 ) 
		{
			calculatedOutput = 1;
		}
		return calculatedOutput;
			
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

		int calculatedOutput = (int)((rawDamage / Mathf.Sqrt((targetEndurance * 8) + targetBonusArmor)) * isStrongerElemScale * isWeakerElemScale * attackBuffScale * defenseBuffScale);

		if (calculatedOutput <= 0)
		{
			calculatedOutput = 1;
		}
		return calculatedOutput;
	}
	public override void EnterState(BattleStateManager state, Dictionary<int, bool> gameObjectId)
	{
		_caster = state.CurrentPlayer;
		_currentSkill = state.SelectedSkill;
		_target = state.SelectedTarget;
		_unitAnimator = state.UnitAnimator;

		//Debug.Log("Entered SkillLogicApplicationState");
		//Debug.Log($"_caster: {_caster.UnitName}");
		//Debug.Log($"_currentSkill: {_currentSkill.SkillName}");
		//Debug.Log($"_target: {_target.UnitName}");

		for (int i = 0; i < state.HeroesGO.Count; i++)
		{
			//Hero tmpUnit = state.HeroesGO[i].GetComponent<Unit>() as Hero;

			//_heroesUnit.Add(tmpUnit);

			_heroesUnit.Add(state.HeroesGO[i].GetComponent<Unit>() as Hero);
		}
		for (int i = 0; i < state.EnemiesGO.Count; i++)
		{
			//Enemy tmpUnit = state.EnemiesGO[i].GetComponent<Unit>() as Enemy;

			//_enemiesUnit.Add(tmpUnit);

			_enemiesUnit.Add(state.EnemiesGO[i].GetComponent<Unit>() as Enemy);
		}
		UpdateState(state);
	}

	public override void UpdateState(BattleStateManager state)
	{
		//Debug.Log("Entered SkillLogicApplicationState - Update State");

		if (_currentSkill is SkillBuff)
		{
			//Debug.Log("Skill is SkillBuff");
			SkillBuff currentSkill = _currentSkill as SkillBuff;
			//Debug.Log("currentSkill info:");
			//Debug.Log($"BuffType: {currentSkill.BuffType}");
			//Debug.Log($"SkillName: {currentSkill.SkillName}");
			//Debug.Log($"AoE: {currentSkill.AoE}");

			if (currentSkill.AoE == AoE.One)
			{
				if (currentSkill.BuffType == Buffs.Attack)
				{
					_target.AttackBuffCount = 3;
					_target.IsAttackBuff = true;

					if (_target == _caster)
					{
						_recentlyCastAttackBuff = true;
					}
					Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on {_target.UnitName}. {_target.UnitName} has gained {_currentSkill.SkillName} for {_target.AttackBuffCount} turn.");
				}
				else if (currentSkill.BuffType == Buffs.Defense)
				{
					_target.DefenseBuffCount = 3;
					_target.IsDefenseBuff = true;

					if (_target == _caster)
					{
						_recentlyCastDefenseBuff = true;
					}
					Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on {_target.UnitName}. {_target.UnitName} has gained {_currentSkill.SkillName} for {_target.DefenseBuffCount} turn.");
				}
			} 
			else if (currentSkill.AoE == AoE.All)
			{
				if (currentSkill.BuffType == Buffs.Attack)
				{
					if (_target is Hero)
					{
						for (int i = 0; i < _heroesUnit.Count; i++)
						{
							_heroesUnit[i].IsAttackBuff = true;
							_heroesUnit[i].AttackBuffCount = 3;

							//Debug.Log($"{_heroesUnit[i].UnitName}.AttackBuffCount: {_heroesUnit[i].AttackBuffCount}");
						}

						Hero target = _target as Hero;

						Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on teammates. All heroes gained {_currentSkill.SkillName} for {target.AttackBuffCount} turn.");
					}
					else if (_target is Enemy)
					{
						for (int i = 0; i < _enemiesUnit.Count; i++)
						{
							_enemiesUnit[i].IsAttackBuff = true;
							_enemiesUnit[i].AttackBuffCount = 3;
						}

						Enemy target = _target as Enemy;

						Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on teammates. All enemies gained {_currentSkill.SkillName} for {target.AttackBuffCount} turn.");
					}

					_recentlyCastAttackBuff = true;
				}
				else if (currentSkill.BuffType == Buffs.Defense)
				{
					if (_target is Hero)
					{
						for (int i = 0; i < _heroesUnit.Count; i++)
						{
							_heroesUnit[i].IsDefenseBuff = true;
							_heroesUnit[i].DefenseBuffCount = 3;
						}

						Hero target = _target as Hero;

						Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on teammates. All heroes gained {_currentSkill.SkillName} for {target.AttackBuffCount} turn.");
					}
					else if (_target is Enemy)
					{
						for (int i = 0; i < _enemiesUnit.Count; i++)
						{
							_enemiesUnit[i].IsDefenseBuff = true;
							_enemiesUnit[i].DefenseBuffCount = 3;
						}

						Enemy target = _target as Enemy;

						Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on teammates. All enemies gained {_currentSkill.SkillName} for {target.AttackBuffCount} turn.");
					}

					_recentlyCastDefenseBuff = true;
				}
			}
			_caster.ManaConsume(currentSkill.ManaCost);
		}
		else if (_currentSkill is SkillHeal)
		{
			SkillHeal currentSkill = _currentSkill as SkillHeal;

			if (currentSkill.AoE == AoE.One)
			{
				int healValue = HealValue(_target, currentSkill.HealthRecoveryRate);

				_target.Heal(healValue);

				Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on {_target.UnitName}. {_target.UnitName} healed {healValue} HP.");
			}
			else if (currentSkill.AoE != AoE.All)
			{
				Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on teammates.");
				if (_target is Hero)
				{
					for (int i = 0; i < _heroesUnit.Count; i++)
					{
						int healValue = HealValue(_heroesUnit[i], currentSkill.HealthRecoveryRate);
						_heroesUnit[i].Heal(healValue);

						Debug.Log($"{_heroesUnit[i]} healed {healValue} turn.");
					}
				}
				else if (_target is Enemy)
				{
					for (int i = 0; i < _enemiesUnit.Count; i++)
					{
						int healValue = HealValue(_heroesUnit[i], currentSkill.HealthRecoveryRate);
						_enemiesUnit[i].Heal(healValue);

						Debug.Log($"{_enemiesUnit[i]} healed {healValue} turn.");
					}
				}
			}
			_caster.ManaConsume(currentSkill.ManaCost);
		}
		else if (_currentSkill is SkillPhysical)
		{
			SkillPhysical currentSkill = _currentSkill as SkillPhysical;
			_unitAnimator[_caster.UnitName].SetTrigger("IsAttacking");

			if (currentSkill.AoE == AoE.One)
			{
				int rawDamage = _caster.PhysicalDamageOutput(currentSkill);
				int calculatedDamage = PhysicalSkillDamageCalculator(_target, rawDamage);
				_target.TakeDamage(calculatedDamage);
				_unitAnimator[_target.UnitName].SetTrigger("IsHurt");

				Debug.Log($"Target: {_target.UnitName}; Target HP: {_target}");
				Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on {_target.UnitName}. {_target.UnitName} received {calculatedDamage} physical damage.");
			}
			else if (currentSkill.AoE == AoE.All)
			{
				int rawDamage = _caster.PhysicalDamageOutput(currentSkill);

				if (_target is Hero)
				{
					Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on all heroes.");
					for (int i = 0; i < _heroesUnit.Count; i++)
					{
						int calculatedDamage = PhysicalSkillDamageCalculator(_heroesUnit[i], rawDamage);
						_heroesUnit[i].TakeDamage(calculatedDamage);

						_unitAnimator[_heroesUnit[i].UnitName].SetTrigger("IsHurt");

						Debug.Log($"Target: {_heroesUnit[i].UnitName}; Target HP: {_heroesUnit[i]}");
						Debug.Log($"{_heroesUnit[i]} received {calculatedDamage} physical damage.");
					}

					 //All enemies gained {_currentSkill.SkillName} for {_target.AttackBuffCount} turn.");
				}
				else
				{
					Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on all enemies.");
					for (int i = 0; i < _enemiesUnit.Count; i++)
					{
						int calculatedDamage = PhysicalSkillDamageCalculator(_enemiesUnit[i], rawDamage);
						_enemiesUnit[i].TakeDamage(calculatedDamage);

						_unitAnimator[_enemiesUnit[i].UnitName].SetTrigger("IsHurt");

						Debug.Log($"Target: {_enemiesUnit[i].UnitName}; Target HP: {_enemiesUnit[i]}");
						Debug.Log($"{_enemiesUnit[i]} received {calculatedDamage} physical damage.");
					}
				}
			}
			_caster.ManaConsume(currentSkill.ManaCost);
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
			int calculatedDamage = PhysicalSkillDamageCalculator(_target, rawDamage);
			_target.TakeDamage(calculatedDamage);

			_unitAnimator[_caster.UnitName].SetTrigger("IsAttacking");
			_unitAnimator[_target.UnitName].SetTrigger("IsHurt");

			Debug.Log($"Target: {_target.UnitName}; Target HP: {_target.CurrentHealth}");
			Debug.Log($"{_target.UnitName} received {calculatedDamage}.");
			Debug.Log($"Target: {_target.UnitName}; Target HP after hit: {_target.CurrentHealth}");
		}
		else if (_currentSkill is SkillMagical)
		{
			SkillMagical currentSkill = _currentSkill as SkillMagical;
			_unitAnimator[_caster.UnitName].SetTrigger("IsAttacking");

			if (currentSkill.AoE == AoE.One)
			{
				Debug.Log($"Target: {_target.UnitName}; Target HP: {_target.CurrentHealth}");
				int rawDamage = _caster.MagicalDamageOutput(_target.CharacterElemType, currentSkill);
				int calculatedDamage = MagicalSkillDamageCalculator(_target, rawDamage);
				_target.TakeDamage(calculatedDamage);

				_unitAnimator[_target.UnitName].SetTrigger("IsHurt");

				Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on {_target.UnitName}. {_target.UnitName} received {calculatedDamage} physical damage.");
				Debug.Log($"Target: {_target.UnitName}; Target HP after hit: {_target.CurrentHealth}");
			}
			else if (currentSkill.AoE == AoE.All)
			{
				if (_target is Hero)
				{
					Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on all heroes.");
					for (int i = 0; i < _heroesUnit.Count; i++)
					{
						int rawDamage = _caster.MagicalDamageOutput(_heroesUnit[i].CharacterElemType, currentSkill);
						int calculatedDamage = MagicalSkillDamageCalculator(_heroesUnit[i], rawDamage);
						_heroesUnit[i].TakeDamage(calculatedDamage);

						_unitAnimator[_heroesUnit[i].UnitName].SetTrigger("IsHurt");

						Debug.Log($"Target: {_heroesUnit[i].UnitName}; Target HP: {_heroesUnit[i]}");
						Debug.Log($"{_heroesUnit[i]} received {calculatedDamage} magical damage.");
					}
				}
				else
				{
					Debug.Log($"{_caster.UnitName} used {_currentSkill.SkillName} on all enemies.");
					for (int i = 0; i < _enemiesUnit.Count; i++)
					{
						int rawDamage = _caster.MagicalDamageOutput(_enemiesUnit[i].CharacterElemType, currentSkill);
						int calculatedDamage = MagicalSkillDamageCalculator(_enemiesUnit[i], rawDamage);
						_enemiesUnit[i].TakeDamage(calculatedDamage);

						_unitAnimator[_enemiesUnit[i].UnitName].SetTrigger("IsHurt");

						Debug.Log($"Target: {_enemiesUnit[i].UnitName}; Target HP: {_enemiesUnit[i]}");
						Debug.Log($"{_enemiesUnit[i]} received {calculatedDamage} magical damage.");
					}
				}
			}
			_caster.ManaConsume(currentSkill.ManaCost);
		}

		List<Skill> casterSkillList = state.SkillsManager.CharacterSkills[_caster.UnitName];

		for (int i = 0; i < casterSkillList.Count; i++)
		{
			if (casterSkillList[i] is SkillPassiveHealthRecovery)
			{
				SkillPassiveHealthRecovery recoverySkill = casterSkillList[i] as SkillPassiveHealthRecovery;
				if (recoverySkill.RegenerationRate == RegenerationRate.Percentage2)
				{
					_caster.Heal(HealValue(_caster, RegenerationRate.Percentage2));
				}
				else
				{
					_caster.Heal(HealValue(_caster, RegenerationRate.Percentage4));
				}
			}
			else if (casterSkillList[i] is SkillPassiveManaRecovery)
			{
				SkillPassiveManaRecovery recoverySkill = casterSkillList[i] as SkillPassiveManaRecovery;
				ManaRecover(recoverySkill.ManaRecoveryRate);
			}
		}

		if (_caster.DefenseBuffCount > 0)
		{
			if (_recentlyCastDefenseBuff)
			{
				_recentlyCastDefenseBuff = false;
			}
			else
			{
				_caster.DefenseBuffCount--;
			}
		}
		if (_caster.AttackBuffCount > 0)
		{
			if (_recentlyCastAttackBuff)
			{
				_recentlyCastAttackBuff = false;
			}
			else
			{
				_caster.AttackBuffCount--;
			}
		}
		state.SwitchState(state.currentActionValueCalculateState);
	}
}
