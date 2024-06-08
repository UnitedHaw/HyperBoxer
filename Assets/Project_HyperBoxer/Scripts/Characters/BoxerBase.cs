using Assets.Project_HyperBoxer.Scripts.Combat;
using Assets.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimationController), typeof(CombatBase))]
public class BoxerBase : MonoBehaviour, IUnitsStateChanger
{
    protected CombatBase _combat;
    protected AnimationController _animationController;
    protected UnitStates _currentState;
    public UnitStates CurrentState => _currentState;

    public event Action<UnitStates> OnStateChanged;

    private void Awake()
    {
        _animationController = GetComponent<AnimationController>();
        _animationController.Init(this, GetComponent<VFX>());

        _combat = GetComponent<CombatBase>();
        _combat.Setup(this, _animationController);
    }

    public void SetState(UnitStates state)
    {
        if (_currentState == state) return;
        _currentState = state;
        switch (state)
        {
            case UnitStates.Combat:
                break;
            case UnitStates.Run:
                break;
        }
        OnStateChanged?.Invoke(state);
    }
}
