using Assets.Project_HyperBoxer.Scripts.Interfaces;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using System;
using UnityEngine;

namespace Assets.Project_HyperBoxer.Scripts.Combat
{
    public abstract class CombatBase : CombatBehaviour, IDamageable, IPurificator
    {
        public event Action<int> OnDamage;
        public event Action OnDied;
        public event Action OnCombatStarted;

        [SerializeField] private int _maxHealth;
        [SerializeField] private int _damage;
        private CombatBase _enemyCombat;
        private int _health;
        private AttackType _currentAttack;

        public AttackType CurrentAttack => _currentAttack;
        public int Damage => _damage;

        private void Start()
        {
            _health = _maxHealth;
            Configure();
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
            _animation.OnAttack += (e) => _currentAttack = e;
        }

        protected override void DeinitCombat()
        {        
            _animation.AttackAnimation.OnDamageFrame -= _enemyCombat.AnimationController.TakeDamage;
            _enemyCombat.OnDied -= Win;
            _enemyCombat.AnimationController.AttackAnimation.OnDeadFrame -= () => Destroy(gameObject);
            _animation.OnAttack -= (e) => _currentAttack = e;
            _enemyCombat = null;
        }

        public void SetDead()
        {
            SetState(UnitStates.Lose);
            OnDied?.Invoke();
        }

        public void Punch(AttackType attackType)
        {
            _animation.SetAttack(attackType);
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

        protected virtual void StartCombat()
        {
            OnCombatStarted?.Invoke();
        }
        protected abstract void Win();
        protected abstract void Lose();
    }
}
