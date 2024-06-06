using Assets.Scripts.Enums;
using System;
using UnityEngine;

[RequireComponent(typeof(AnimationController), typeof(CombatOld))]
public abstract class Unit : MonoBehaviour, IUnitsStateChanger
{
    public event Action<UnitStates> OnStateChanged;

    
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _health;
    [SerializeField] private float _triggerDistance;
    [SerializeField] private InfoPanel _infoPanel;
    private Renderer _renderer;
    private MaterialPropertyBlock _materialPropertyBlock;
    private AnimationController _animation;
    private CombatOld _combat;
    private VFX _vfx;
    private Unit _targetUnit;
    private UnitStates _currentState;
    private bool _move;
    private Quaternion _startDirection;
    private string _infoText;

    public CombatOld Combat => _combat;
    public int Health => _health;
    public UnitStates CurrentState => _currentState;

    public void Init(float moveSpeed = 1, float scale = 1)
    {
        _moveSpeed = moveSpeed;
        transform.localScale = new Vector3(scale, scale, scale);

        //_materialPropertyBlock.SetColor("_BaseColor", _district.Color);
    }

    private void Awake()
    {
        _animation = GetComponent<AnimationController>();
        _combat = GetComponent<CombatOld>();
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
        //UpdateInfo();
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
                SetMove(true);
                break;
            case UnitStates.Wait:
                break;
            case UnitStates.Win:
                UnitWin();
                break;
        }
        OnStateChanged?.Invoke(state);
    }

    public abstract void UnitWin();

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
            EnterUnitHandler(unit);
        }
    }

    protected virtual void EnterUnitHandler(Unit unit)
    {
        if (unit.CurrentState != UnitStates.Lose)
        {
            _targetUnit = unit;
            _combat.SetEnemyTarget(_targetUnit.Combat);
            SetState(UnitStates.Combat);
        }
    }

    private void ResetTargetUnit()
    {
        _targetUnit = null;
        _combat.RemoveEnemyTarget();
    }

    public void UpdateInfo()
    {
        var target = _targetUnit is null ? "null" : _targetUnit.name;
        _infoText = $"Current state: {_currentState} \n Target enemy: {target} \n Current attack: {_combat.CurrentAttack}";
        _infoPanel.SetText(_infoText);
    }
}
