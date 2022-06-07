using System;
using Core;
using Movement;
using UnityEngine;

namespace Combat
{
    public class Fighter : MonoBehaviour, IAction
    {

         Transform target;
        [SerializeField]  float attackRange = 2f;
        [SerializeField]  float timeBetweenAttacks = 1f;
        [SerializeField]  float weaponDamage = 10f;

        private float timeSinceLastAttack = 0f;
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            
            if (!GetIsInRange())
            {
                GetComponent<PlayerMover>().MoveTo(target.position);
            }

            else
            {
                GetComponent<PlayerMover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if(timeSinceLastAttack> timeBetweenAttacks)
            {
                // triggers the hit animation event
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0f;
                

            }
        }
        // animation event
        void Hit()
        {
            Health healthComponent = target.GetComponent<Health>();
            healthComponent.TakeDamage(weaponDamage);
        }
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < attackRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }



        public void Cancel()
        {
            target = null;
        }
    }
}
