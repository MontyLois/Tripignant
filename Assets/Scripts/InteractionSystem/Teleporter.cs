using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class Teleporter : MonoBehaviour, ITeleport
{
    [SerializeField] private Transform destination;
    
    public void Teleport(Character playerTransform)
    {
        playerTransform.SetPosition(destination.position);
        playerTransform.SetRotation(destination.rotation);
    }
}
