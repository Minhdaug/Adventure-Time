using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BattlePlayerTurnState : BattleBaseState
{
	private bool init = false;

	private Dictionary<GameObject, Skill> _mercySkillButtons = new Dictionary<GameObject, Skill>();
	private Dictionary<GameObject, Skill> _eniSkillButtons = new Dictionary<GameObject, Skill>();
	private Dictionary<GameObject, Skill> _iniriusSkillButtons = new Dictionary<GameObject, Skill>();

	private Hero _currentPlayer;

	private SkillsManager _skillsManager;

	private Dictionary<Unit, GameObject> _heroesSelectFrames = new Dictionary<Unit, GameObject>();
	private Dictionary<Unit, GameObject> _enemiesSelectFrames = new Dictionary<Unit, GameObject>();
	private Dictionary<int, bool> _currentStateOfChars = new Dictionary<int, bool>();
	private BattleStateManager _currentState;

	private Coroutine _targetSelectionCoroutine;


	public override void EnterState(BattleStateManager state, Dictionary<int, bool> gameObjectId)
	{
		_currentStateOfChars = gameObjectId;
		_currentPlayer = state.CurrentPlayer as Hero;
		_currentState = state;

		if (!init)
		{
			_skillsManager = state.SkillsManager;

			_heroesSelectFrames = state.HeroesButtonsDict;
			_enemiesSelectFrames = state.EnemiesButtonsDict;


			foreach (KeyValuePair<int, bool> kvp in _currentStateOfChars)
			{
				Unit target = (EditorUtility.InstanceIDToObject(kvp.Key) as GameObject).GetComponent<Unit>();
				Dictionary<GameObject, Skill> tempDictButtonSkill = new Dictionary<GameObject, Skill>();

				if (target is Hero)
				{
					List<Skill> playerSkills = _skillsManager.GetHeroSkill(target.UnitName, _currentPlayer.CurrentLevel);

					//Debug.Log($"Current character: {target.UnitName}");

					//foreach (Skill skill in playerSkills)
					//{
					//	Debug.Log($"Skill name: {skill.SkillName}");
					//}

					int count = 0;

					for (int i = 0; i < playerSkills.Count; i++)
					{
						if (playerSkills[i] is not SkillPassiveHealthRecovery && playerSkills[i] is not SkillPassiveManaRecovery)
						{
							//GameObject tempSkill = UnityEngine.Object.Instantiate(state.SkillButtonPrefab, state.SkillButtonTransforms[count]);
							GameObject tempSkill = UnityEngine.Object.Instantiate(state.SkillButtonPrefab, state.SkillButtonTransforms[count]);
							Button btn = tempSkill.GetComponent<Button>();

							TextMeshProUGUI skillBtnText = btn.GetComponentInChildren<TextMeshProUGUI>();
							skillBtnText.text = playerSkills[i].SkillName;

							tempSkill.SetActive(false);

							tempDictButtonSkill[tempSkill] = playerSkills[i];
							count += 1;

							if (playerSkills[i] is SkillBuff || playerSkills[i] is SkillHeal)
							{
								btn.onClick.AddListener( delegate {SetSelectFrameActive(_heroesSelectFrames); });
								btn.onClick.AddListener( delegate { AssignSkill(tempSkill); });
							}
							else
							{
								btn.onClick.AddListener(delegate { SetSelectFrameActive(_enemiesSelectFrames); });
								btn.onClick.AddListener(delegate { AssignSkill(tempSkill); });
							}
						}
					}
					switch (target.UnitName)
					{
						case "Mercy":
							_mercySkillButtons = tempDictButtonSkill;
							break;
						case "Inirius":
							_iniriusSkillButtons = tempDictButtonSkill;
							break;
						case "Eni":
							_eniSkillButtons = tempDictButtonSkill;
							break;
					}
				}
			}

			foreach (KeyValuePair<Unit, GameObject> kvp in _heroesSelectFrames)
			{
				kvp.Value.GetComponentInChildren<Button>().onClick.AddListener(delegate
				{
					AssignTarget(kvp.Key, _heroesSelectFrames);
				});
			}
			foreach (KeyValuePair<Unit, GameObject> kvp in _enemiesSelectFrames)
			{
				kvp.Value.GetComponentInChildren<Button>().onClick.AddListener(delegate
				{
					AssignTarget(kvp.Key, _enemiesSelectFrames);
				});
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
				SetButtonsActive(_mercySkillButtons);
				break;
			case "Inirius":
				SetButtonsActive(_iniriusSkillButtons);
				break;
			case "Eni":
				SetButtonsActive(_eniSkillButtons);
				break;
		}
	}
	public void AssignTarget(Unit target, Dictionary<Unit, GameObject> dict)
	{
		_currentState.SelectedTarget = target;
		SetSelectFrameInactive(dict);
		_currentState.SwitchState(_currentState.currentSkillLogicApplicationState);
	}
	public void AssignSkill (GameObject buttonGO)
	{
		Dictionary<GameObject, Skill> tempSkillDict;
		switch (_currentPlayer.UnitName)
		{
			case "Mercy":
				tempSkillDict = _mercySkillButtons;
				break;
			case "Inirius":
				tempSkillDict = _iniriusSkillButtons;
				break;
			case "Eni":
				tempSkillDict = _eniSkillButtons;
				break;
			default:
				throw new System.Exception("Hero does not exists");
		}
		_currentState.SelectedSkill = tempSkillDict[buttonGO];
	}
	public void SetSelectFrameActive (Dictionary<Unit, GameObject> dict)
	{
		foreach (KeyValuePair<Unit, GameObject> entry in dict) 
		{
			entry.Value.SetActive(true);
		}
		_targetSelectionCoroutine = _currentState.StartCoroutine(WaitingForTargetSelection(dict));
	}
	public void SetSelectFrameInactive(Dictionary<Unit, GameObject> frames)
	{
		foreach (KeyValuePair<Unit, GameObject> entry in frames)
		{
			entry.Value.SetActive(false);
		}
	}
	public void SetButtonsActive(Dictionary<GameObject, Skill> buttons)
	{
		foreach (KeyValuePair<GameObject, Skill> kvp in buttons)
		{
			kvp.Key.SetActive(true);
		}
	}
	public void SetButtonsInactive(Dictionary<GameObject, Skill> buttons)
	{
		foreach (KeyValuePair<GameObject, Skill> kvp in buttons)
		{
			kvp.Key.SetActive(false);
		}
	}

	public System.Collections.IEnumerator WaitingForTargetSelection(Dictionary<Unit, GameObject> dict)
	{
		while (true)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector3 clickPosition = Input.mousePosition;
				//Debug.Log($"Mouse clicked at: {Input.mousePosition}");

				bool clickedOutside = true;

				foreach (KeyValuePair<Unit, GameObject> kvp in dict)
				{
					Vector3 buttonPosition = Camera.main.WorldToScreenPoint(kvp.Value.transform.position);

					float buttonSize = kvp.Value.GetComponent<RectTransform>().rect.width;

					if (clickPosition.x >= buttonPosition.x - buttonSize / 2f &&
						clickPosition.x <= buttonPosition.x + buttonSize / 2f &&
						clickPosition.y >= buttonPosition.y - buttonSize / 2f &&
						clickPosition.y <= buttonPosition.y + buttonSize / 2f)
					{
						clickedOutside = false;
						break;
					}
				}
				if (clickedOutside)
				{
					//Debug.Log("Clicked outside of the selection buttons...");

					foreach (KeyValuePair<Unit, GameObject> kvp in dict)
					{
						kvp.Value.SetActive(false);
					}
				}
				else
				{
					//Debug.Log("Selected a target");
					switch (_currentPlayer.UnitName)
					{
						case "Mercy":
							SetButtonsInactive(_mercySkillButtons);
							break;
						case "Inirius":
							SetButtonsInactive(_iniriusSkillButtons);
							break;
						case "Eni":
							SetButtonsInactive(_eniSkillButtons);
							break;
					}
				}
				break;
			}
			else
			{
				yield return null;
			}
		}
	}
}