using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _damageVfx;
    [SerializeField] private ParticleSystem _leftPunchVfx;
    [SerializeField] private ParticleSystem _rightPunchVfx;
    [SerializeField] private ParticleSystem _deadVfx;
    public void TakeDamageVFX()
    {
        _damageVfx?.Play();
    }

    public void PunchVFX(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.Left:
                _leftPunchVfx?.Play();
                break;
            case AttackType.Right:
                _rightPunchVfx?.Play();
                break;
        }
    }

    public void DeadVFX()
    {
        _deadVfx.transform.parent = null;
        _deadVfx?.Play();
    }

}
