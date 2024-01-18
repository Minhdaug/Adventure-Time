using System.Collections.Generic;
using UnityEngine;

public abstract class BattleBaseState
{
    public abstract void EnterState(BattleStateManager state, Dictionary<int, bool> gameObjectId);

	public abstract void UpdateState(BattleStateManager state);
}
