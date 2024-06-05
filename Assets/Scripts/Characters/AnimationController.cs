using Assets.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEngine.Rendering.DebugUI;

public class AnimationController : MonoBehaviour
{
    public event Action OnDamage;
    public event Action<AttackType> OnAttack;

    [SerializeField] private Transform _rootModel;
    [SerializeField] private float _transitionTime;
    [SerializeField] private float _rootMotionTransition;
    private Animator _animator;
    private AttackAnimation _attackAnimation;
    private IUnitStateEvent _unitEvent;
    private VFX _vfx;
    private readonly int _moveKey = Animator.StringToHash("Move");
    private readonly int _idleKey = Animator.StringToHash("Idle");
    private readonly int _attackLeftKey = Animator.StringToHash("LeftSide");
    private readonly int _attackRightKey = Animator.StringToHash("RightUpper");
    private readonly int _damageRightKey = Animator.StringToHash("LightHead");
    private readonly int _damageLeftKey = Animator.StringToHash("Stomach");
    private readonly int _deathKey = Animator.StringToHash("Death");

    public Transform RootModel => _rootModel;
    public Animator CombatAnimator => _animator;
    public AttackAnimation AttackAnimation => _attackAnimation;


    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _attackAnimation = _animator.GetComponent<AttackAnimation>();
    }

    public void Init(IUnitStateEvent unitEvent, VFX vfx)
    {
        _unitEvent = unitEvent;
        _vfx = vfx;
        _attackAnimation.Init(_vfx);
        _unitEvent.OnStateChanged += SetState;
    }

    private void SetState(UnitStates state)
    {
        if (_animator == null) return;

        if (state == UnitStates.Combat)
        {
            _animator.applyRootMotion = true;
        }else
        {
            _animator.applyRootMotion = false;
        }
    }

    public void SetMove(bool value)
    {
        if (_animator == null) return;

        _animator.SetBool(_moveKey, value);
    }

    public void SetAttack(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.Left:
                _animator.CrossFade(_attackLeftKey, _transitionTime, 0);
                break;
            case AttackType.Right:
                _animator.CrossFade(_attackRightKey, _transitionTime, 0);
                break;        
        }

        OnAttack?.Invoke(attackType);
    }

    public void TakeDamage(AttackType attackType)
    {
        if(_animator == null) return;

        switch (attackType)
        {
            case AttackType.Left:
                _animator.CrossFade(_damageRightKey, _transitionTime, 0);
                break;
            case AttackType.Right:
                _animator.CrossFade(_damageLeftKey, _transitionTime, 0);
                break;
        }

        OnDamage?.Invoke();
    }

    public void DeathAnim()
    {
        _animator.CrossFade(_deathKey, .3f, 0);
    }
}