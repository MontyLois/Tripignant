using System;
using UnityEngine;

namespace PuzzleSystem.Sample.RadioSample
{
    public class PlayerExample : MonoBehaviour
    {
        [SerializeField]
        private Radio radio;

        [SerializeField]
        private Battery battery;

        private void Update()
        {
            // Donner
            if (Input.GetKeyDown(KeyCode.Space))
                radio.SetBattery(battery);
        }
    }
}