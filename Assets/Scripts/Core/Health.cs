using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class Health
    {
        [SerializeField] private int _maxHealth;

        public int MaxHealth => _maxHealth;
        public int CurrentHealth { get; private set; }

        public event Action OnZeroHealth;
        public event Action<int> OnHealthChanged;

        public Health(int maxHealth)
        {
            _maxHealth = maxHealth;
            Initialize();
        }

        public void Initialize()
        {
            CurrentHealth = _maxHealth;
        }
        
        public void TakeDamage(int damageAmount)
        {
            CurrentHealth -= damageAmount;

            if (CurrentHealth < 0)
                CurrentHealth = 0;
            
            OnHealthChanged?.Invoke(CurrentHealth);
            
            if(CurrentHealth == 0)
                OnZeroHealth?.Invoke();
        }

        public void Heal(int healAmount)
        {
            CurrentHealth += healAmount;

            if (CurrentHealth > _maxHealth)
                CurrentHealth = _maxHealth;
            
            OnHealthChanged?.Invoke(CurrentHealth);
        }
    }
}