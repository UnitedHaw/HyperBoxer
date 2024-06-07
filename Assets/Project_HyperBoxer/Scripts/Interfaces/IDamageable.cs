using System;

namespace Assets.Scripts.Interfaces
{
    public interface IDamageable
    {
        public event Action<int> OnDamage;
        public event Action OnDied;

        public int Damage {  get; }
        public void TakeDamage(int damage);
        public void SetDead();
    }
}
