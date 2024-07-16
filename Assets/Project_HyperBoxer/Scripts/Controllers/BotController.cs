using Assets.Project_HyperBoxer.Scripts.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Project_HyperBoxer.Scripts.Controllers
{
    public class BotController : CharacterController
    {
        public BotController(BoxerBase boxer) : base(boxer)
        {

        }

        public void ChangeCharacter(BoxerBase boxerBase)
        {
            _character = boxerBase;
        }

        protected override void Purify()
        {
            
        }
    }
}
