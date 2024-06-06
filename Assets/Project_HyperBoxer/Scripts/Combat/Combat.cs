using Assets.Scripts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Project_HyperBoxer.Scripts.Combat
{
    public class Combat : CombatBase
    {
        protected override void Lose()
        {
            _animation.DeathAnim();
        }

        protected override void StartCombat()
        {
            SetState(UnitStates.Combat);
        }

        protected override void Win()
        {
            DeinitCombat();
        }
    }
}
