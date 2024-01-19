using Assets.script.model;
using Assets.script.services;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class BattleStateManager : MonoBehaviour
{
    BattleBaseState _currentBattleState;

    public BattleCheckEndConditionState currentCheckEndConditionState = new BattleCheckEndConditionState();
    public BattleActionValueCalculateState currentActionValueCalculateState = new BattleActionValueCalculateState();
    public BattleSkillLogicApplicationState currentSkillLogicApplicationState = new BattleSkillLogicApplicationState();
    public BattleEnemyTurnState currentEnemyTurnState = new BattleEnemyTurnState();
    public BattlePlayerTurnState currentPlayerTurnState = new BattlePlayerTurnState();
    public BattleAnimationState currentAnimationState = new BattleAnimationState();
    private SkillsManager _skillManager;
	[SerializeField] TextMeshProUGUI _combatLogText;
	public TextMeshProUGUI CombatLogText
	{
		get { return _combatLogText; }
		set { _combatLogText = value; }
	}

	public class CharacterCombatData 
	{
		public readonly Dictionary<string, int> characterLvl = new Dictionary<string, int>();
		public readonly List<string> enemiesInAct = new List<string>();
	}

	//private CombatService _combatService;
	private JsonDataService _jsonDataService;

	private Dictionary<int, Animator> _unitAnimator = new Dictionary<int, Animator>();
	public Dictionary<int, Animator> UnitAnimator
	{
		get { return _unitAnimator; }
		set { _unitAnimator = value; }
	}

	//[SerializeField]

	public SkillsManager SkillsManager
	{
		get { return _skillManager; }
	}
    //protected SkillsManager skillManager;

    [Header("Spawn Positions")]
    [SerializeField] List<Transform> _playerTransforms;
    [SerializeField] List<Transform> _enemyTransforms;

	[Header("Spawn Prefabs")]
    [SerializeField] List<GameObject> _heroesGO;
	public List<GameObject> HeroesGO
	{
		get { return _heroesGO; }
		set { _heroesGO = value; }
	}
	[SerializeField] List<GameObject> _enemiesGO;
	public List<GameObject> EnemiesGO
	{
		get { return _enemiesGO; }
		set { _enemiesGO = value; }
	}

	private List<GameObject> _heroesSpawnGO = new List<GameObject>();
	public List<GameObject> HeroesSpawnGO
	{
		get { return _heroesSpawnGO; }
		set { _heroesSpawnGO = value; }
	}

	private List<GameObject> _enemiesSpawnGO = new List<GameObject>();
	public List<GameObject> EnemiesSpawnGO
	{
		get { return _enemiesSpawnGO; }
		set { _enemiesSpawnGO = value; }
	}

	private Unit _currentPlayer;
	public Unit CurrentPlayer
	{
		get { return _currentPlayer; }
		set { _currentPlayer = value; }
	}

	private int _currentPlayerID;
	public int CurrentPlayerID
	{
		get { return _currentPlayerID; }
		set { _currentPlayerID = value; }
	}

	private Skill _selectedSkill;
	public Skill SelectedSkill
	{
		get { return _selectedSkill; }
		set { _selectedSkill = value; }
	}
	private Unit _selectedTarget;
	public Unit SelectedTarget
	{
		get { return _selectedTarget; }
		set { _selectedTarget = value; }
	}

	private int _selectedTargetID;
	public int SelectedTargetID
	{
		get { return _selectedTargetID; }
		set { _selectedTargetID = value; }
	}

	private Dictionary<int, bool> _currentGameObjects = new Dictionary<int, bool>();

	[Header("Speed Frame Prefabs")]
	[SerializeField] List<Transform> _speedFrameTransforms;
	public List<Transform> SpeedFrameTransform
	{
		get { return _speedFrameTransforms; }
		set { _speedFrameTransforms = value; }
	}

	[Header("Skill Button Transforms")]
	[SerializeField] List<Transform> _skillButtonTransforms;
	public List<Transform> SkillButtonTransforms
	{
		get { return _skillButtonTransforms; }
		set { _skillButtonTransforms = value;}
	}

	[SerializeField] GameObject _skillButtonPrefab;
	public GameObject SkillButtonPrefab
	{
		get { return _skillButtonPrefab; }
		set { _skillButtonPrefab = value; }
	}

	[SerializeField] Canvas _hudCanvas;
	public Canvas HUDCanvas
	{
		get { return _hudCanvas; }
		set { _hudCanvas = value; }
	}

	[SerializeField] GameObject _selectFrame;

	[Header("Target Selection Frames")]
	[SerializeField] List<Transform> _heroesSelectFrameTransforms;
	[SerializeField] List<Transform> _enemiesSelectFrameTransforms;

	private Dictionary<Unit, GameObject> _heroesButtonsDict = new Dictionary<Unit, GameObject>();

	public Dictionary<Unit, GameObject> HeroesButtonsDict
	{
		get { return _heroesButtonsDict; }
		set { _heroesButtonsDict = value; }
	}

	private Dictionary<Unit, GameObject> _enemiesButtonsDict = new Dictionary<Unit, GameObject>();

	public Dictionary<Unit, GameObject> EnemiesButtonsDict
	{
		get { return _enemiesButtonsDict; }
		set { _enemiesButtonsDict = value; }
	}

	// Start is called before the first frame update
	void Start()
    {
		//Debug.Log($"data: {_jsonDataService.LoadData<Type>("/staticSaveData.json", false)}");

		InitPrefabs();
		_skillManager = GetComponentInParent<SkillsManager>();
		//Debug.Log(files);

		//var data = _jsonDataService.LoadData<Type>("/staticSaveData.json", false);

		//Debug.Log(data);

		_currentBattleState = currentActionValueCalculateState;
        _currentBattleState.EnterState(this, _currentGameObjects);
    }

    void InitPrefabs()
	{
		string path = Application.persistentDataPath;

		//Debug.Log(path);
		var files = System.IO.Directory.GetFiles(path);
		string filePath = path + "\\combatData.json";

		if (files != null)
		{
			foreach (var item in files)
			{
				if (item == filePath)
				{
					string jsonContent = System.IO.File.ReadAllText(filePath);
					CombatData charCombatData = JsonUtility.FromJson<CombatData>(jsonContent);

					if (charCombatData != null)
					{
						foreach (KeyValuePair<string, int> kvp in charCombatData.characterLvl)
						{
							//Debug.Log($"kvp.Key: {kvp.Key}");

							foreach (GameObject itemGO in _heroesGO)
							{
								Hero itemUnit = itemGO.GetComponent<Unit>() as Hero;
								
								// Set Hero current level.
								//itemUnit.CurrentLevel = kvp.Value;

								if (itemUnit.UnitName == kvp.Key)
								{
									_heroesSpawnGO.Add(itemGO);
								}
							}

							//if (go != null)
							//{
							//	Debug.Log($"go: {go.GetComponent<Unit>().UnitName}");
							//}

							//Debug.Log($"kvp.Value: {kvp.Value}");
						}

						foreach (string enemyName in charCombatData.enemiesInAct)
						{
							//Debug.Log($"enemyName: {enemyName}");

							foreach (GameObject itemGO in _enemiesGO)
							{
								Unit itemUnit = itemGO.GetComponent<Unit>();

								if (itemUnit.UnitName == enemyName)
								{
									_enemiesSpawnGO.Add(itemGO);
								}
							}
						}
					}
				}
			}
		}

		int loop_player;
		int loop_enemy;
		if (_playerTransforms.Count == _heroesSpawnGO.Count || _playerTransforms.Count > _heroesSpawnGO.Count)
		{
			loop_player = _heroesSpawnGO.Count;
		}
		else
		{
			loop_player = _playerTransforms.Count;
		}
		if (_enemyTransforms.Count == _enemiesSpawnGO.Count || _enemyTransforms.Count > _enemiesSpawnGO.Count)
		{
			loop_enemy = _enemiesSpawnGO.Count;
		}
		else
		{
			loop_enemy = _enemyTransforms.Count;
		}

		for (int i = 0; i < loop_player; i++)
		{
			GameObject tempGO = Instantiate(_heroesSpawnGO[i], _playerTransforms[i]);
			SpriteRenderer sr = tempGO.GetComponent<SpriteRenderer>();

			//Unit tempUnit = tempGO.GetComponent<Unit>();
			Animator tempAnimator = tempGO.GetComponent<Animator>();
			int id = tempGO.GetInstanceID();
			_unitAnimator.Add(id, tempAnimator);

			if (sr == null)
			{
				Debug.Log("sr is null");
			}

			sr.sortingLayerName = "Characters";

			switch (i)
			{
				case 0:
					sr.sortingOrder = 1;
					break;
				case 1:
					sr.sortingOrder = 0;
					break;
				case 2:
					sr.sortingOrder = 2;
					break;
			}
			//int id = tempGO.GetInstanceID();
			_currentGameObjects[id] = true;

			GameObject tempSelectFrameGO = Instantiate(_selectFrame, _heroesSelectFrameTransforms[i]);
			tempSelectFrameGO.SetActive(false);
			_heroesButtonsDict.Add(tempGO.GetComponent<Unit>(), tempSelectFrameGO);
		}
		for (int i = 0; i < loop_enemy; i++)
		{
			GameObject tempGO = Instantiate(_enemiesSpawnGO[i], _enemyTransforms[i]);
			SpriteRenderer sr = tempGO.GetComponent<SpriteRenderer>();
			sr.sortingLayerName = "Characters";

			//Unit tempUnit = tempGO.GetComponent<Unit>();
			Animator tempAnimator = tempGO.GetComponent<Animator>();
			int id = tempGO.GetInstanceID();
			_unitAnimator.Add(id, tempAnimator);

			if (sr == null)
			{
				Debug.Log("sr is null");
				Debug.Log($"{sr}");
			}

			switch (i)
			{
				case 0:
					sr.sortingOrder = 1;
					break;
				case 1:
					sr.sortingOrder = 0;
					break;
				case 2:
					sr.sortingOrder = 2;
					break;
				case 3:
					sr.sortingOrder = 0;
					break;
				case 4:
					sr.sortingOrder = 2;
					break;
			}
			//int id = tempGO.GetInstanceID();
			_currentGameObjects[id] = true;

			GameObject tempSelectFrameGO = Instantiate(_selectFrame, _enemiesSelectFrameTransforms[i]);
			tempSelectFrameGO.SetActive(false);
			_enemiesButtonsDict.Add(tempGO.GetComponent<Unit>(), tempSelectFrameGO);
		}
	}

    public void SwitchState(BattleBaseState state)
    {
        _currentBattleState = state;
        _currentBattleState.EnterState(this, _currentGameObjects);
    }
}
