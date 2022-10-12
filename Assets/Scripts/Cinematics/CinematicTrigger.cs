using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Playables;
using RPG.Saving;
using Newtonsoft.Json.Linq;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, IJsonSaveable
    {
        bool hasTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!hasTriggered && other.tag == "Player")
            {
                hasTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }           
        }

        public JToken CaptureAsJToken()
        {
            return JToken.FromObject(hasTriggered);
        }

        public void RestoreFromJToken(JToken state)
        {
            hasTriggered = state.ToObject<bool>();
        }
    }
}
