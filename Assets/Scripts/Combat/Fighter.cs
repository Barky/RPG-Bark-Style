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
        

        private void Update()
        {

            if (target == null) return;
            
            if (!GetIsInRange())
            {
                GetComponent<PlayerMover>().MoveTo(target.position);
            }

            else
            {
                GetComponent<PlayerMover>().Cancel();
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

        public void Cancel()
        {
            target = null;
        }
    }
}
