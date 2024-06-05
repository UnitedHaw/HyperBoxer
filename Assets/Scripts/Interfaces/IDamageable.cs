using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface IDamageable
    {
        public event Action<int> OnDamage;
        public event Action OnDied;

        public int Damage {  get; }
        public void TakeDamage();
        public void SetDead();
    }
}
