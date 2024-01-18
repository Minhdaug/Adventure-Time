using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	private Dictionary<string, Animator> _unitAnimator = new Dictionary<string, Animator>();
	public Dictionary<string, Animator> UnitAnimator
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
	}
	[SerializeField] List<GameObject> _enemiesGO;
	public List<GameObject> EnemiesGO
	{
		get { return _enemiesGO; }
	}


	private Unit _currentPlayer;
	public Unit CurrentPlayer
	{
		get { return _currentPlayer; }
		set { _currentPlayer = value; }
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
		InitPrefabs();
		_skillManager = GetComponentInParent<SkillsManager>();

        _currentBattleState = currentActionValueCalculateState;
        _currentBattleState.EnterState(this, _currentGameObjects);
    }

    void InitPrefabs()
	{
		int loop_player;
		int loop_enemy;
		if (_playerTransforms.Count == _heroesGO.Count || _playerTransforms.Count > _heroesGO.Count)
		{
			loop_player = _heroesGO.Count;
		}
		else
		{
			loop_player = _playerTransforms.Count;
		}
		if (_enemyTransforms.Count == _enemiesGO.Count || _enemyTransforms.Count > _enemiesGO.Count)
		{
			loop_enemy = _enemiesGO.Count;
		}
		else
		{
			loop_enemy = _enemyTransforms.Count;
		}

		for (int i = 0; i < loop_player; i++)
		{
			GameObject tempGO = Instantiate(_heroesGO[i], _playerTransforms[i]);
			SpriteRenderer sr = tempGO.GetComponent<SpriteRenderer>();

			Unit tempUnit = tempGO.GetComponent<Unit>();
			Animator tempAnimator = tempGO.GetComponent<Animator>();
			_unitAnimator.Add(tempUnit.UnitName, tempAnimator);

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
			int id = tempGO.GetInstanceID();
			_currentGameObjects[id] = true;

			GameObject tempSelectFrameGO = Instantiate(_selectFrame, _heroesSelectFrameTransforms[i]);
			tempSelectFrameGO.SetActive(false);
			_heroesButtonsDict.Add(tempGO.GetComponent<Unit>(), tempSelectFrameGO);
		}
		for (int i = 0; i < loop_enemy; i++)
		{
			GameObject tempGO = Instantiate(_enemiesGO[i], _enemyTransforms[i]);
			SpriteRenderer sr = tempGO.GetComponent<SpriteRenderer>();
			sr.sortingLayerName = "Characters";

			Unit tempUnit = tempGO.GetComponent<Unit>();
			Animator tempAnimator = tempGO.GetComponent<Animator>();
			_unitAnimator.Add(tempUnit.UnitName, tempAnimator);

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
			int id = tempGO.GetInstanceID();
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

	public void AssignSwitchStateOnEndAnimation(Animator animator)
	{

	}
}
