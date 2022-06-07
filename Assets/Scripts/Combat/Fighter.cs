using System;
using Core;
using Movement;
using UnityEngine;

namespace Combat
{
    public class Fighter : MonoBehaviour, IAction
    {

         Transform target;
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float timeBetweenAttacks = 1f;

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
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0f;
            }
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

        // animation event
        void Hit()
        {
            
        }

        public void Cancel()
        {
            target = null;
        }
    }
}
