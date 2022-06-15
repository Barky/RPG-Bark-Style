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
        [SerializeField] private float suspicionTime = 3f;
        [SerializeField] private float waypointTolerance = 1f;
        [SerializeField] private float waypointDwellTime = 3f;
        
        [Range(0,1)] [SerializeField] private float patrolSpeedFraction = 0.2f;


        private Fighter fighter;
        private GameObject player;
        private Health health;
        private PlayerMover mover;

        [SerializeField] private PatrolPath patrolpath;
        
        private Vector3 guardingPosition;

        private int currentWaypointIndex = 0;

        private float timeSinceLastPlayerSaw = Mathf.Infinity;
        private float timeSinceArrivedWaypoint = Mathf.Infinity;

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
                AttackBehaviour();
            }
            else if(timeSinceLastPlayerSaw < suspicionTime)

            {
                SuspicionBehaviour();
            }
            
            else
            {
                PatrolBehaviour();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastPlayerSaw += Time.deltaTime;
            timeSinceArrivedWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 NextPosition = guardingPosition;

            if (patrolpath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedWaypoint = 0f;
                    CycleWaypoint();
                }
                NextPosition = GetCurrentWaypoint();
            }
            if(timeSinceArrivedWaypoint >  waypointDwellTime)
            {
                mover.StartMoveAction(NextPosition, patrolSpeedFraction);
            }
        }


        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolpath.GetNextIndex(currentWaypointIndex);
        }
        private Vector3 GetCurrentWaypoint()
        {
            return patrolpath.GetWayPoint(currentWaypointIndex);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            timeSinceLastPlayerSaw = 0;
            fighter.Attack(player);
        }

        private bool InAttackRange()
        {
            float distanceToPlayer =  Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }
        
        
        
    }
    
    
}
