using System.Collections.Generic;
using UnityEngine;

namespace PuzzleSystem.Sample.RadioSample
{
    public class Radio : MonoBehaviour
    {
        public bool HasBattery => battery != null;

        private Battery battery;

        [SerializeField]
        private AudioSource audioSource;

        public void SetBattery(Battery batteryToGive)
        {
            this.battery = batteryToGive;
        }

        public void PlayClip(AudioClip audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}