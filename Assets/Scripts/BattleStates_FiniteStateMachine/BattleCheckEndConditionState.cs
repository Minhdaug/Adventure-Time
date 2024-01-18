using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleCheckEndConditionState : BattleBaseState
{
	public override void EnterState(BattleStateManager state, Dictionary<int, bool> gameObjectId)
	{
		if (state is null)
		{
			throw new ArgumentNullException(nameof(state));
		}
	}

	public override void UpdateState(BattleStateManager state)
	{

	}
}
