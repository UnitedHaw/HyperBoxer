using Assets.Project_HyperBoxer.Scripts.UI;
using Assets.Scripts.Enums;
using System;

namespace Assets.Project_HyperBoxer.Scripts
{
    public class PlayerInput : IDisposable
    {
        public event Action<AttackType> OnButtonPressed;

        private CombatControlWindow _combatControl;
        public PlayerInput() { }
        public PlayerInput(CombatControlWindow combatControl)
        {
            _combatControl = combatControl;

            _combatControl.RightHandPunch += () => OnButtonPressed?.Invoke(AttackType.Right);
            _combatControl.LeftHandPunch += () => OnButtonPressed?.Invoke(AttackType.Left);
        }

        public void Dispose()
        {
            _combatControl.RightHandPunch -= () => OnButtonPressed?.Invoke(AttackType.Right);
            _combatControl.LeftHandPunch -= () => OnButtonPressed?.Invoke(AttackType.Left);
        }
    }
}
