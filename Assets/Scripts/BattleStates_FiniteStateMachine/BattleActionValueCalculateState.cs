using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BattleActionValueCalculateState : BattleBaseState
{
	private Dictionary<int, Unit> _unitStats = new Dictionary<int, Unit>();
	//private Dictionary<int, GameObject> _gameObjectsWithIds = new Dictionary<int, GameObject>();
	private Queue<Tuple<int, Unit>> _turnQueue = new Queue<Tuple<int, Unit>>();
	private Dictionary<int, bool> _gameObjectId;
	private List<GameObject> _currentSpeedFrames = new List<GameObject>();
	private int _queueLimit = 5;

	public override void EnterState(BattleStateManager state, Dictionary<int, bool> gameObjectId)
	{
		Debug.Log("Entered EnterState of BattleActionValueCalculateState.");

		_gameObjectId = gameObjectId;
		//if (_gameObjectsWithIds == null)
		if (_unitStats.Count <= 0)
		{
			Debug.Log("First time assigning called.");
			foreach (KeyValuePair<int, bool> pair in gameObjectId) 
			{
				int key = pair.Key;
				bool value = pair.Value;

				Unit target = (EditorUtility.InstanceIDToObject(key) as GameObject).GetComponent<Unit>();
				if (target != null)
				{
					_unitStats[key] = target;
				}
				else
				{
					throw new ArgumentException($"Cannot find GameObject with Instance ID: {key}.");
				}
			}
		}
		else
		{
			Debug.Log("Updating GameObject States.");
			UpdateGOStates(gameObjectId);
		}

		Debug.Log("Switching to UpdateState.");
		UpdateState(state);
	}

	public override void UpdateState(BattleStateManager state)
	{
		Debug.Log("Entered UpdateState of BattleActionValueCalculateState");

		foreach (GameObject frame in _currentSpeedFrames)
		{
			UnityEngine.Object.Destroy(frame);
		}

		while (_turnQueue.Count < _queueLimit)
		{
			var sorted = _unitStats.OrderBy(i => i.Value.CurrentActionValue);

			Dictionary<int, Unit> tempDict = sorted.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

			_unitStats = tempDict;
			
			int toUpdate = _unitStats.First().Value.CurrentActionValue;

			foreach (Unit unit in _unitStats.Values)
			{
				unit.UpdateActionValue(toUpdate);
			}

			_turnQueue.Enqueue(new Tuple<int, Unit>(_unitStats.First().Key, _unitStats.First().Value));

			//Debug.Log($"Added turn for: {_unitStats.First().Value.UnitName}");

			_unitStats.First().Value.CalculateActionValue();
		}

		Tuple<int, Unit> currentUnit = _turnQueue.Dequeue();

		int unitId = currentUnit.Item1;
		Unit unitStat = currentUnit.Item2;

		if (unitStat is Hero)
		{
			state.CurrentPlayer = unitStat as Hero;
		}
		else if (unitStat is Enemy) 
		{
			state.CurrentPlayer = unitStat as Enemy;
		}

		_currentSpeedFrames.Add(UnityEngine.Object.Instantiate(unitStat.SpeedFrame, state.SpeedFrameTransform[0]));

		int count = 1;

		foreach (Tuple<int, Unit> unit in _turnQueue)
		{
			_currentSpeedFrames.Add(UnityEngine.Object.Instantiate(unit.Item2.SpeedFrame, state.SpeedFrameTransform[count]));
			count++;
		}

		if (unitStat is Hero) 
		{
			state.SwitchState(state.currentPlayerTurnState);
			// Enter state of Player Turn
		} 
		else
		{
			state.SwitchState(state.currentEnemyTurnState);
			// Enter state of enemy turn
		}

		Debug.Log("Ready to switch to another state...");
	}


	public void UpdateGOStates(Dictionary<int, bool> gameObjectId)
	{
		foreach (KeyValuePair<int, bool> pair in gameObjectId)
		{
			int key = pair.Key;
			bool value = pair.Value;

			if (_unitStats.ContainsKey(key) && value == false)
			{
				_unitStats.Remove(key);

				Queue<Tuple<int, Unit>> tempQueue = new Queue<Tuple<int, Unit>>();

				while (_turnQueue.Count > 0) 
				{
					Tuple<int, Unit> tempTuple = _turnQueue.Dequeue();

					if (tempTuple.Item1 != key)
					{
						tempQueue.Enqueue(tempTuple);
					}
				}
				_turnQueue = tempQueue;
			}
		}
	}
}
