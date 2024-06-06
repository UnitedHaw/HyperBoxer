using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using static UnityEngine.CullingGroup;
using Assets.Scripts.Interfaces;

public class Combat : MonoBehaviour, IDamageable
{
    public event Action<int> OnDamage;
    public event Action OnDied;

    [SerializeField] private int _damage;
    [SerializeField, Range(1, 10)] private int _attackInterval;
    [SerializeField] private float _takeDamageCooldown;

    private AnimationController _animation;
    private IUnitsStateChanger _unitState;
    private VFX _vfx;
    private Combat _enemyCombat;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Coroutine _combatCorutine;

    private AttackType _currentAttack;

    private int _health;
    private int _attackAmount;
    private float _damageDelay = 0;
    

    public AttackType CurrentAttack => _currentAttack;
    public AnimationController AnimationController => _animation;
    public Coroutine CombatCourutine => _combatCorutine;
    public int Damage => _damage;

    private void Awake()
    {
        _attackAmount = Enum.GetNames(typeof(AttackType)).Length;
    }

    public void Init(AnimationController animation, int health, IUnitsStateChanger unitsStateChanger, VFX vfx)
    {
        _health = health;
        _animation = animation;
        _vfx = vfx;
        _unitState = unitsStateChanger;
        _unitState.OnStateChanged += SetState;
        _animation.OnAttack += (e) => _currentAttack = e;
        _animation.OnDamage += TakeDamage;
    }

    private void SetState(UnitStates state)
    {

        switch (state)
        {
            case UnitStates.None:
                StopCombat();
                break;
            case UnitStates.Combat:
                StartCombat();
                break;
            case UnitStates.Win:
                StopCombat();
                break;
            case UnitStates.Lose:
                StopCombat();
                break;
        }
    }

    private void Update()
    {
        if (_enemyCombat != null && _unitState.CurrentState == UnitStates.Combat)
            transform.LookAt(_enemyCombat.AnimationController.RootModel.position, Vector3.up);
    }

    private void StartCombat()
    {
        _startPosition = _animation.AttackAnimation.transform.localPosition;
        _startRotation = _animation.AttackAnimation.transform.rotation;
        _combatCorutine = StartCoroutine(CombatCorutine());
    }

    private void StopCombat()
    { 
        StopCoroutine(_combatCorutine);
    }

    private void WinCombat()
    {
        if(_unitState.CurrentState != UnitStates.Lose)
            _unitState.SetState(UnitStates.Win);
    }

    public void SetEnemyTarget(Combat combatEnemy)
    {
        _enemyCombat = combatEnemy;
        _animation.AttackAnimation.OnDamageFrame += _enemyCombat.AnimationController.TakeDamage;
        _enemyCombat.OnDied += WinCombat;
        _enemyCombat.AnimationController.AttackAnimation.OnDeadFrame += Continue;
    }

    public void RemoveEnemyTarget()
    {
        if(_enemyCombat == null) return;

        _animation.AttackAnimation.OnDamageFrame -= _enemyCombat.AnimationController.TakeDamage;
        _enemyCombat.OnDied -= StopCombat;
        _enemyCombat.AnimationController.AttackAnimation.OnDeadFrame -= Continue;
        _enemyCombat = null;
    }

    private IEnumerator CombatCorutine()
    {
        while (true)
        {
            var index = Random.Range(0, _attackAmount);
            yield return null;

            var time = Random.Range(0, (float)_attackInterval);
            _animation.SetAttack((AttackType)index);
            yield return new WaitForSeconds(time);
            yield return new WaitForSeconds(_damageDelay);
            _damageDelay = 0;
        }
    }

    private void Continue()
    {
        _animation.AttackAnimation.transform.localPosition = _startPosition;
        _animation.AttackAnimation.transform.rotation = _startRotation;
        RemoveEnemyTarget();
        _unitState.SetState(UnitStates.Run);
    }

    public void TakeDamage()
    {
        _damageDelay = _takeDamageCooldown;
        OnDamage?.Invoke(_damage);
        _vfx.TakeDamageVFX();

        _health = Mathf.Clamp(_health - _damage, 0, int.MaxValue);

        if (_health <= 0)
        {
            SetDead();
        }
    }

    public void SetDead()
    {
        OnDied?.Invoke();
        _unitState.SetState(UnitStates.Lose);           
        _animation.DeathAnim();
        _animation.AttackAnimation.OnDeadFrame += Dead;
        //transform.localScale = Vector3.zero;
    }

    private void Dead()
    {
        _animation.AttackAnimation.OnDeadFrame -= Dead;
        _vfx.DeadVFX();
        _unitState.SetState(UnitStates.None);
        transform.GetComponent<Collider>().enabled = false;
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        RemoveEnemyTarget();
        _unitState.OnStateChanged -= SetState;
        _animation.OnAttack -= (e) => _currentAttack = e;
        _animation.OnDamage -= TakeDamage;
    }
}
