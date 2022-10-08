using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicsControlRemover : MonoBehaviour
    {
        GameObject player;
        PlayableDirector playableDirector;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            playableDirector = GetComponent<PlayableDirector>();

            playableDirector.played += DisableControl;
            playableDirector.stopped += EnableControl;
        }

        void DisableControl(PlayableDirector pd)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }
    }

}
