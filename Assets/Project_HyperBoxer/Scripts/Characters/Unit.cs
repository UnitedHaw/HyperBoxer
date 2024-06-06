using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(AnimationController), typeof(Combat))]
public class Unit : MonoBehaviour, IUnitsStateChanger
{
    public event Action<UnitStates> OnStateChanged;

    
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _health;
    [SerializeField] private float _triggerDistance;
    [SerializeField] private InfoPanel _infoPanel;
    private Renderer _renderer;
    private MaterialPropertyBlock _materialPropertyBlock;
    private AnimationController _animation;
    private Combat _combat;
    private VFX _vfx;
    private Unit _targetUnit;
    private UnitStates _currentState;
    //private IDistrict _district;
    private bool _move;
    private bool _checkNeighbor;
    private Quaternion _startDirection;
    private string _infoText;

    public Combat Combat => _combat;
    public int Health => _health;
    //public IDistrict District => _district;
    //public DistrictState DistrictState => _district.District;
    public UnitStates CurrentState => _currentState;

    public void Init(float moveSpeed = 1, float scale = 1)
    {
        //_district = district;
        _moveSpeed = moveSpeed;
        transform.localScale = new Vector3(scale, scale, scale);

        //_materialPropertyBlock.SetColor("_BaseColor", _district.Color);
    }

    private void Awake()
    {
        _animation = GetComponent<AnimationController>();
        _combat = GetComponent<Combat>();
        _vfx = GetComponent<VFX>();
        _renderer = GetComponentInChildren<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
        _combat.Init(_animation, _health, this, _vfx);
        _animation.Init(this, _vfx);
        SetState(UnitStates.Run);
    }

    private void Update()
    {
        MoveForward();
        CheckNeighbor();
        UpdateInfo();
    }

    public void SetState(UnitStates state)
    {
        if(_currentState == state) return;
        _currentState = state;
        switch (state)
        {
            case UnitStates.None:
                SetMove(false);
                break;
            case UnitStates.Combat:
                _startDirection = transform.rotation;
                SetMove(false);
                break;
            case UnitStates.Run:
                ResetTargetUnit();
                SetMove(true);
                break;
            case UnitStates.Wait:
                WaitInQueue();
                break;
            case UnitStates.Win:
                transform.rotation = _startDirection;
                break;
        }
        OnStateChanged?.Invoke(state);
    }

    private void WaitInQueue()
    {
        SetMove(false);
        _targetUnit.OnStateChanged += AwaitHandler;
        _checkNeighbor = true;
    }

    private async void AwaitHandler(UnitStates value)
    {
        switch (value)
        {
            case UnitStates.None:
                _targetUnit.OnStateChanged -= AwaitHandler;
                SetState(UnitStates.Run);               
                break;
        }
    }

    private void CheckNeighbor()
    {
        if (_checkNeighbor)
        {
            if (_targetUnit == null)
            {
                return;
            }

            var distance = Vector3.Distance(transform.position, _targetUnit.transform.position);
            if (distance > _triggerDistance)
            {
                SetMove(true);
            }else
            {
                SetMove(false);
            }
        }
    }

    private void MoveForward()
    {
        if (_move)
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, Time.deltaTime * _moveSpeed);
    }

    private void SetMove(bool value)
    {
        if(_move == value) return;

        _animation.SetMove(value);
        _move = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            if (_currentState == UnitStates.Combat 
                || _currentState == UnitStates.Wait 
                || _currentState == UnitStates.Lose)
                return;

            EnterUnitHandler(unit);
        }
    }

    private void EnterUnitHandler(Unit unit)
    {
        //if (unit.DistrictState == _groupType)
        //{
        //    SetState(UnitStates.Wait);
        //}

        if (unit.CurrentState != UnitStates.Lose)
        {
            _targetUnit = unit;
            _combat.SetEnemyTarget(_targetUnit.Combat);
            SetState(UnitStates.Combat);
        }

    }

    private void ResetTargetUnit()
    {
        //if (_targetUnit == null) return;

        _targetUnit = null;
        _checkNeighbor = false;
        _combat.RemoveEnemyTarget();
    }

    public void UpdateInfo()
    {
        var target = _targetUnit is null ? "null" : _targetUnit.name;
        _infoText = $"Current state: {_currentState} \n Target enemy: {target} \n Current attack: {_combat.CurrentAttack}";
        _infoPanel.SetText(_infoText);
    }
}
