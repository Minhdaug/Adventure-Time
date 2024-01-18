using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BattleEnemyTurnState : BattleBaseState
{
	private bool init = false;
	private Enemy _currentPlayer;
	private List<Hero> _heroUnit = new List<Hero>();
	private List<Enemy> _enemyUnit = new List<Enemy>();

	private Dictionary<Enemy, List<Skill>> _enemySkillDict = new Dictionary<Enemy, List<Skill>>();

	public override void EnterState(BattleStateManager state, Dictionary<int, bool> gameObjectId)
	{
		if (!init)
		{
			foreach (KeyValuePair<int, bool> kvp in gameObjectId)
			{
				GameObject tempGO = EditorUtility.InstanceIDToObject(kvp.Key) as GameObject;
				Unit tempUnit = tempGO.GetComponent<Unit>();

				if (tempUnit is Enemy)
				{
					_enemyUnit.Add(tempUnit as Enemy);
				}
				else if (tempUnit is Hero)
				{
					_heroUnit.Add(tempUnit as Hero);
				}
				else
				{
					throw new System.ArgumentException("Unit is not Enemy / Hero, or doesn't exits.");
				}
			}

			foreach (Enemy enemy in _enemyUnit)
			{
				List<Skill> tempSkillList = state.SkillsManager.GetEnemySkill(enemy.UnitName);

				_enemySkillDict[enemy] = tempSkillList;
			}
		}

		_currentPlayer = state.CurrentPlayer as Enemy;

		UpdateState(state);
	}

	public override void UpdateState(BattleStateManager state)
	{
		int randomSkillIndex = Random.Range(0, _enemySkillDict[_currentPlayer].Count);
		state.SelectedSkill = _enemySkillDict[_currentPlayer][randomSkillIndex];

		Skill skill = state.SelectedSkill;

		if (skill is SkillPhysical || skill is SkillMagical || skill is NormalAttack)
		{
			int targetHeroIndex = Random.Range(0, _heroUnit.Count);
			state.SelectedTarget = _heroUnit[targetHeroIndex];
		}
		else if (skill is SkillBuff || skill is SkillHeal)
		{
			int targetEnemyIndex = Random.Range(0, _enemyUnit.Count);
			state.SelectedTarget = _enemyUnit[targetEnemyIndex];
		}
		else if (skill is SkillPassiveHealthRecovery || skill is SkillPassiveManaRecovery)
		{
		}
		else
		{
			throw new System.ArgumentException("Unknown skill called.");
		}

		state.SwitchState(state.currentSkillLogicApplicationState);
	}
}
