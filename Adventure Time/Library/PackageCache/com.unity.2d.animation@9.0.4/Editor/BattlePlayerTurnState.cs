using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class BattlePlayerTurnState : BattleBaseState
{
	private bool init = false;

	private List<GameObject> _mercySkillButtons = new List<GameObject>();
	private List<GameObject> _eniSkillButtons = new List<GameObject>();
	private List<GameObject> _iniriusSkillButtons = new List<GameObject>();


	private Hero _currentPlayer;
	private SkillsManager _skillsManager;

	private Dictionary<int, GameObject> _playerFrames = new Dictionary<int, GameObject>();
	private Dictionary<int, GameObject> _enemyFrames = new Dictionary<int, GameObject>();
	private Dictionary<int, GameObject> _allGO = new Dictionary<int, GameObject>();
	private Dictionary<int, bool> _currentStateOfChars = new Dictionary<int, bool>();

	public override void EnterState(BattleStateManager state, Dictionary<int, bool> gameObjectId)
	{
		_currentStateOfChars = gameObjectId;

		if (!init)
		{
			_currentPlayer = state.CurrentHeroTurn;
			_skillsManager = state.SkillsManager;

			foreach (KeyValuePair<int, bool> kvp in _currentStateOfChars)
			{
				if (kvp.Value) 
				{
					GameObject targetGameObject = EditorUtility.InstanceIDToObject(kvp.Key) as GameObject;
					_allGO.Add(kvp.Key, targetGameObject);

					GameObject frame = UnityEngine.Object.Instantiate(targetGameObject.GetComponent<Unit>().SelectionFrame);
					frame.SetActive(false);

					if (targetGameObject.GetComponent<Unit>() is Hero) 
					{
						_playerFrames[kvp.Key] = frame;
					}
					else
					{
						_enemyFrames[kvp.Key] = frame;
					}
				}
			}
			init = true;
		} 
		UpdateState(state);
	}
	public override void UpdateState(BattleStateManager state)
	{
		switch (_currentPlayer.UnitName)
		{
			case "Mercy":
				if (_mercySkillButtons.Count == 0)
				{
					List<Skill> playerSkills = _skillsManager.GetHeroSkill(_currentPlayer.UnitName, _currentPlayer.CurrentLevel);

					GameObject tempObject = UnityEngine.Object.Instantiate(state.SkillButtonPrefab, state.SkillButtonTransforms[0]);
					
					Text normalAttackButtonText = tempObject.GetComponentInChildren<Text>();
					
					if (tempObject != null)
					{
						Debug.Log($"tempObject is not null");
						normalAttackButtonText.text = "Normal Attack";
						
					}

					//for (int i = 0; i < playerSkills.Count; i++)
					//{

					//}
					//_mercySkillButtons.Add();

					//for (int i = 1; i < state.SkillButtonTransforms.Count; i++)
					//{
					//	GameObject skillButton = UnityEngine.Object.Instantiate(state.SkillButtonPrefab, state.SkillButtonTransforms[i]);
					//}
				}
				break;
			case "Inirius":
				break;
			case "Eni":
				if (_eniSkillButtons.Count == 0)
				{
					List<Skill> playerSkills = _skillsManager.GetHeroSkill(_currentPlayer.UnitName, _currentPlayer.CurrentLevel);

					GameObject tempObject = UnityEngine.Object.Instantiate(state.SkillButtonPrefab, state.SkillButtonTransforms[0]);

					TextMeshProUGUI normalAttackButtonText = tempObject.GetComponentInChildren<TextMeshProUGUI>();

					if (tempObject != null)
					{
						Debug.Log($"tempObject is not null");
						normalAttackButtonText.text = "Normal Attack";

					}

					//for (int i = 0; i < playerSkills.Count; i++)
					//{

					//}
					//_mercySkillButtons.Add();

					//for (int i = 1; i < state.SkillButtonTransforms.Count; i++)
					//{
					//	GameObject skillButton = UnityEngine.Object.Instantiate(state.SkillButtonPrefab, state.SkillButtonTransforms[i]);
					//}
				}
				break;
		}

		//for (int i = 0; i <  playerSkills.Count; i++)
		//{
		//	Debug.Log($"Skill {i}: {playerSkills[i].SkillName}");

		//	if (playerSkills[i].GetType() != typeof(SkillPassiveHealthRecovery) && playerSkills[i].GetType() != typeof(SkillPassiveManaRecovery))
		//	{
		//		GameObject.Instantiate(state.SkillButtonPrefab, state.SkillButtonTransforms[i+1]);
		//	}
		//}
	}
}
