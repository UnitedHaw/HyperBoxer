using Assets.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackAnimation : MonoBehaviour
{
    public event Action<AttackType> OnDamageFrame;
    public event Action OnDeadFrame;

    private Combat _unitCombat;
    private VFX _vfx;

    public void Init(VFX vfx)
    {
        _vfx = vfx;
    }

    private void Awake()
    {
        _unitCombat = GetComponentInParent<Combat>();
    }

    public void TargetDamage()
    {
        OnDamageFrame?.Invoke(_unitCombat.CurrentAttack);
        _vfx.PunchVFX(_unitCombat.CurrentAttack);
    }

    public void Dead()
    {
        OnDeadFrame?.Invoke();
    }
}
