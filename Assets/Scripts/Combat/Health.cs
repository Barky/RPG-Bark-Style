using UnityEngine;

namespace Combat
{
    public class Health : MonoBehaviour
    {

        [SerializeField]  float healthPoints = 100f;

        private bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }
        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max((healthPoints - damage), 0);
            Debug.Log("enemy health = " +healthPoints);

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
        }
    }
}
