using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [Header ("Scene Transition Tuning")]
        [SerializeField] int sceneToLoad = -1;

        [Tooltip("Player will spawn at the spawn point transform.position be sure to rotate so Z axis faces forwards")]
        [SerializeField] Transform spawnPoint;

        [Tooltip("Use the Destination Identifer Enum A, B, C, D to choose the corisponding portal to 'spawn' into on the scene to load")]
        [SerializeField] DestinationIdentifier destination;

        [Header("Scene Transition Effect Tuning")]
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = .5f;

        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }   
        }

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load is not set");
            }

            Fader fader = FindObjectOfType<Fader>();

            DontDestroyOnLoad(gameObject);

            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) { continue; }
                if (portal.destination != destination) { continue; }
                return portal;
            }

            return null;
        }
    }
}

