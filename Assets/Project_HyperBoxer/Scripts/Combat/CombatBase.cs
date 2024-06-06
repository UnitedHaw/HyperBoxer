using Assets.Project_HyperBoxer.Scripts.Interfaces;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace Assets.Project_HyperBoxer.Scripts.Combat
{
    public abstract class CombatBase : CombatBehaviour, IDamageable, IPurificator
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _damage;
        private CombatBase _enemyCombat;
        private int _health;
        public int Damage => _damage;

        public event Action<int> OnDamage;
        public event Action OnDied;

        private void Start()
        {
            _health = _maxHealth;
        }

        public void SetState(UnitStates states)
        {
            switch (states)
            {
                case UnitStates.Combat:
                    break;
                    StartCombat(); 
                    break;
                case UnitStates.Win:
                    Win();
                    break;
                case UnitStates.Lose:
                    Lose();
                    break;
            }
        }

        protected override void SetupTarget(CombatBase enemy)
        {
            _enemyCombat = enemy;
            _animation.AttackAnimation.OnDamageFrame += _enemyCombat.AnimationController.TakeDamage;
            _enemyCombat.OnDied += Win;
            _enemyCombat.AnimationController.AttackAnimation.OnDeadFrame += () => Destroy(gameObject);
        }

        protected override void DeinitCombat()
        {        
            _animation.AttackAnimation.OnDamageFrame -= _enemyCombat.AnimationController.TakeDamage;
            _enemyCombat.OnDied -= Win;
            _enemyCombat.AnimationController.AttackAnimation.OnDeadFrame -= () => Destroy(gameObject);
            _enemyCombat = null;
        }

        public void SetDead()
        {
            SetState(UnitStates.Lose);
            OnDied?.Invoke();
        }

        public void TakeDamage(int damage)
        {
            OnDamage?.Invoke(damage);

            _health = Mathf.Clamp(_maxHealth - _damage, 0, int.MaxValue);

            if (_health <= 0)
            {
                SetDead();
            }
        }

        private void OnDisable()
        {
            Purify();
        }

        public virtual void Configure()
        {
            _unitStatechanger.OnStateChanged += SetState;
        }

        public virtual void Purify()
        {
            _unitStatechanger.OnStateChanged -= SetState;
        }

        protected abstract void StartCombat();
        protected abstract void Win();
        protected abstract void Lose();
    }
}
