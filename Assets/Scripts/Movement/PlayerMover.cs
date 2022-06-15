using System;
using Combat;
using Core;
using UnityEngine;
using UnityEngine.AI;

namespace Movement
{
    public class PlayerMover : MonoBehaviour, IAction
    {
        private NavMeshAgent nav;
        private Animator anim;
        private Health health;
        
        [SerializeField] private Transform target_;
        private Ray lastRay;

        [SerializeField] private float maximumSpeed = 6f;

        private void Start()
        {
            nav = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            nav.enabled = !health.IsDead();
            UpdateAnimator();
        }
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            nav.destination = destination;
            nav.speed = maximumSpeed * Mathf.Clamp01(speedFraction);
            nav.isStopped = false;

        }
        public void Cancel()
        {
            nav.isStopped = true;
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            
            GetComponent<ActionScheduler>().StartAction(this);
            
            MoveTo(destination, speedFraction);
        }

        void UpdateAnimator()
        {
            Vector3 velocity = nav.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed_ = localVelocity.z;
        
            anim.SetFloat("forwardSpeed", speed_);
        
        }

    }
}
