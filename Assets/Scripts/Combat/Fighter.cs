using System;
using Core;
using Movement;
using UnityEngine;

namespace Combat
{
    public class Fighter : MonoBehaviour, IAction
    {

         Health target;
        [SerializeField]  float attackRange = 2f;
        [SerializeField]  float timeBetweenAttacks = 1f;
        [SerializeField]  float weaponDamage = 10f;

        private float timeSinceLastAttack = Mathf.Infinity;
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime; 
            if (target == null) return;
            if (target.IsDead()) return;
            if (!GetIsInRange())
            {
                GetComponent<PlayerMover>().MoveTo(target.transform.position);
            }

            else
            {
                GetComponent<PlayerMover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if(timeSinceLastAttack> timeBetweenAttacks)
            {
                // triggers the hit animation event
                GetComponent<Animator>().ResetTrigger("stopAttack");
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
                

            }
        }
        // animation event
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(weaponDamage);
        }
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < attackRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {

            if (combatTarget == null)
            {
                return false;
            }
            
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }



        public void Cancel()
        {
            print("cancel attack calıstı");
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
        }
    }
}
