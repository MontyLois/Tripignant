using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerCustscene : MonoBehaviour
{
    [SerializeField] private PlayableDirector cinematic;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            cinematic.Play();
        }
    }
}
