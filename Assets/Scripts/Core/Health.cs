using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Saving;
using Newtonsoft.Json.Linq;

namespace RPG.Core
{
    public class Health : MonoBehaviour, IJsonSaveable
    {
        [Header("Tuning")]
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints -= damage;
            if (healthPoints <= 0) { Die(); }

            Debug.Log(this.name + ": " + healthPoints);
        }

        private void Die()
        {
            if (isDead) { return; }

            healthPoints = 0;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public JToken CaptureAsJToken()
        {
            return JToken.FromObject(healthPoints);
        }


        public void RestoreFromJToken(JToken state)
        {
            healthPoints = state.ToObject<float>();
            if (healthPoints == 0) { Die(); }
        }

    }
}