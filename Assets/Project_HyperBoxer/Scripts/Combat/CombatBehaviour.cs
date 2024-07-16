using Assets.Project_HyperBoxer.Scripts;
using Assets.Project_HyperBoxer.Scripts.Combat;
using Assets.Scripts.Enums;
using UnityEngine;

public abstract class CombatBehaviour : MonoBehaviour
{
    protected AnimationController _animation;
    protected IUnitsStateChanger _unitStatechanger;
    protected IControllerInitilizer _characterController;
    public AnimationController AnimationController => _animation;

    public void Setup(IUnitsStateChanger unitStateChanger, AnimationController animation)
    {
        _animation = animation;
        _unitStatechanger = unitStateChanger;
    }

    protected abstract void SetupTarget(CombatBase enemy);
    protected abstract void DeinitCombat();
}
