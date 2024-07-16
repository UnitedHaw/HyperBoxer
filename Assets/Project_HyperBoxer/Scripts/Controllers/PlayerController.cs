using Assets.Project_HyperBoxer.Scripts.Combat;
using Assets.Project_HyperBoxer.Scripts.UI;
using System;
namespace Assets.Project_HyperBoxer.Scripts.Controllers
{
    public class PlayerController : CharacterController
    {
        private CombatControlWindow _combatControlWindow;
        private PlayerInput _playerInput;

        public PlayerController(BoxerBase boxer, CombatControlWindow combatControlWindow) : base(boxer)
        {
            _combatControlWindow = combatControlWindow;
            _playerInput = new PlayerInput(_combatControlWindow);
            _playerInput.OnButtonPressed += _character.Combat.Punch;
        }

        protected override void Purify()
        {
            _playerInput.OnButtonPressed -= _character.Combat.Punch;
            _playerInput.Dispose();
        }
    }
}
