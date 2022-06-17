using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        private bool isTriggered;
        private void OnTriggerEnter(Collider other)
        {
            print("calısti");
            GetComponent<PlayableDirector>().Play();
            if (!isTriggered && other.CompareTag("Player"))
            {
                isTriggered = true;
                
                
            }
        }
    }
}
