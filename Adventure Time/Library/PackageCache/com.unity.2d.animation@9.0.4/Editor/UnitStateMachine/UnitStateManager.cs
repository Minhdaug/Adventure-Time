using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateManager : MonoBehaviour
{
    UnitBaseState _currentState;

    UnitIdleState _idleState;
    UnitSkillSelectState _skillSelectState;
    UnitTargetSelectState _targetSelectState;
    UnitAttackState _attackState;
    UnitHurtState _hurtState;
    UnitDeathState _deathState;
    UnitUseItemState _useItemState;

    // Start is called before the first frame update
    void Start()
    {
        _currentState = _idleState;

        _currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
