using UnityEngine;

namespace Core
{
    public class Health : MonoBehaviour
    {

        [SerializeField]  float healthPoints = 100f;

        [SerializeField] private bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }
        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max((healthPoints - damage), 0);
            Debug.Log(transform.tag + " health = " +healthPoints);

            if (healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;
            
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
    
}
