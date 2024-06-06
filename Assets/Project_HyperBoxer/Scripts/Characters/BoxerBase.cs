using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimationController), typeof(CombatOld))]
public abstract class BoxerBase : MonoBehaviour
{
    protected CombatOld _combat;
    protected AnimationController _animationController;
    protected Unit _targetUnit;

    private void Awake()
    {
        _animationController = GetComponent<AnimationController>();
        _combat = GetComponent<CombatOld>();
    }
}
