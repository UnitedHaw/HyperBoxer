using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Project_HyperBoxer.Scripts.Controllers
{
    public abstract class CharacterController : IDisposable
    {
        protected BoxerBase _character;
        public BoxerBase Character => _character;

        public CharacterController(BoxerBase boxer)
        {
            _character = boxer;
        }

        public void Dispose()
        {
            Purify();
        }

        protected abstract void Purify();
    }
}
