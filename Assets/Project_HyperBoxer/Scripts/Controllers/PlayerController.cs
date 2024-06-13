using Assets.Project_HyperBoxer.Scripts.Combat;
using Assets.Project_HyperBoxer.Scripts.UI;
using System;
namespace Assets.Project_HyperBoxer.Scripts.Controllers
{
    public class PlayerController : ICharacterController
    {
        private CombatBase _combat;
        private CombatControlWindow _combatControlWindow;
        private PlayerInput _playerInput;

        public PlayerController(CombatControlWindow combatControlWindow)
        {
            _combatControlWindow = combatControlWindow;
            _playerInput = new PlayerInput(_combatControlWindow);
        }

        public void Dispose()
        {
            _playerInput.OnButtonPressed -= _combat.Punch;
            _playerInput.Dispose();
        }

        public void Setup(CombatBase combatBase)
        {
            _combat = combatBase;
            _playerInput.OnButtonPressed += _combat.Punch;
        }
    }
}
