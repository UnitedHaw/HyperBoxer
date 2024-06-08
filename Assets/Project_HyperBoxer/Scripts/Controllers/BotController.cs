﻿using Assets.Project_HyperBoxer.Scripts.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Project_HyperBoxer.Scripts.Controllers
{
    public class BotController : ICharacterController
    {
        private CombatBase _combat;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Setup(CombatBase combatBase)
        {
            _combat = combatBase;
        }
    }
}
