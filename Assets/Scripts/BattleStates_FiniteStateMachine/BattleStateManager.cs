using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public SkillsManager SkillsManager
	{
		get { return _skillManager; }
	}
    //protected SkillsManager skillManager;

    [Header("Spawn Positions")]
    [SerializeField] List<Transform> _playerTransforms;
    [SerializeField] List<Transform> _enemyTransforms;

	[Header("Spawn Prefabs")]
    [SerializeField] List<GameObject> _playersGO;
	[SerializeField] List<GameObject> _enemyGO;

	private Hero _currentHeroTurn;
	public Hero CurrentHeroTurn
	{
		get { return _currentHeroTurn; }
		set { _currentHeroTurn = value; }
	}

	private Unit _currentEnemyTurn;
	public Unit CurrentEnemyTurn
	{
		get { return _currentEnemyTurn; }
		set { _currentEnemyTurn = value; }
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
		if (_playerTransforms.Count == _playersGO.Count || _playerTransforms.Count > _playersGO.Count)
		{
			loop_player = _playersGO.Count;
		}
		else
		{
			loop_player = _playerTransforms.Count;
		}
		if (_enemyTransforms.Count == _enemyGO.Count || _enemyTransforms.Count > _enemyGO.Count)
		{
			loop_enemy = _enemyGO.Count;
		}
		else
		{
			loop_enemy = _enemyTransforms.Count;
		}

		for (int i = 0; i < loop_player; i++)
		{
			int id = Instantiate(_playersGO[i], _playerTransforms[i]).GetInstanceID();
			_currentGameObjects[id] = true;
		}
		for (int i = 0; i < loop_enemy; i++)
		{
			int id = Instantiate(_enemyGO[i], _enemyTransforms[i]).GetInstanceID();
			_currentGameObjects[id] = true;
		}
	}

    public void SwitchState(BattleBaseState state)
    {
        _currentBattleState = state;
        _currentBattleState.EnterState(this, _currentGameObjects);
    }
}
