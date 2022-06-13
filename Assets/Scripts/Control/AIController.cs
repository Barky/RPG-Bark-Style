using System;
using Combat;
using Core;
using Movement;
using UnityEditor;
using UnityEngine;

namespace Control
{
    public class AIController : MonoBehaviour
    {

        [SerializeField] private float chaseDistance = 5f;
        private Fighter fighter;
        private GameObject player;
        private Health health;
        private PlayerMover mover;
        
        private Vector3 guardingPosition;
        private void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<PlayerMover>();
            
            player = GameObject.FindGameObjectWithTag("Player");

            guardingPosition = transform.position;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        private void Update()
        {
            if (health.IsDead()) return;
            
            if (InAttackRange() && fighter.CanAttack(player))
            {
                fighter.Attack(player);

            }
            else
            {
                mover.StartMoveAction(guardingPosition);
            }
        }

        private bool InAttackRange()
        {
            float distanceToPlayer =  Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }
        
        
        
    }
    
    
}
