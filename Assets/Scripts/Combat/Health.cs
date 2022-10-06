using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints -= damage;
            if (healthPoints <= 0)
            {
                Die();
            }

            Debug.Log(this.name + ": " + healthPoints);
        }

        private void Die()
        {
            if (isDead) return;
            healthPoints = 0;
            GetComponent<Animator>().SetTrigger("die");
            isDead = true;
        }
    }
}

