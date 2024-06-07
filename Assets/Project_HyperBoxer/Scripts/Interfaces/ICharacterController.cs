using Assets.Project_HyperBoxer.Scripts.Combat;
using System;

namespace Assets.Project_HyperBoxer.Scripts
{
    public interface ICharacterController : IDisposable
    {
        public void Setup(CombatBase combatBase);
    }
}
